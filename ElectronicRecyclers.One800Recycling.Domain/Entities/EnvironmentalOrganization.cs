using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using ElectronicRecyclers.One800Recycling.Domain.Common;
using ElectronicRecyclers.One800Recycling.Domain.ValueObjects;
using ElectronicRecyclers.One800Recycling.Domain.Exceptions;
using ElectronicRecyclers.One800Recycling.Domain.Common.Helper;
using NHibernate;

namespace ElectronicRecyclers.One800Recycling.Domain.Entities
{
    [Serializable]
    public abstract class EnvironmentalOrganization : DomainObject
    {
        public virtual string Name { get; set; }

        public virtual string Description { get; set; }

        public virtual Phone Telephone { get; set; }

        public virtual Phone Fax { get; set; }

        public virtual Address Address { get; set; }

        public virtual bool IsMailingAddress { get; set; }

        public virtual string LogoImageUrl { get; set; }

        public virtual string WebsiteUrl { get; set; }

        public virtual string ImportBatchName { get; set; }

        public virtual bool IsEnabled { get; set; }

        protected EnvironmentalOrganization() { }

        protected EnvironmentalOrganization(EnvironmentalOrganization organization)
        {
            Name = organization.Name;
            Description = organization.Description;
            Telephone = organization.Telephone;
            Fax = organization.Fax;
            Address = organization.Address;
            IsMailingAddress = organization.IsMailingAddress;
            LogoImageUrl = organization.LogoImageUrl;
            WebsiteUrl = organization.WebsiteUrl;
            ImportBatchName = organization.ImportBatchName;
            IsEnabled = organization.IsEnabled;

            AddHoursOfOperations(organization.GetHoursOfOperations());
            AddMaterials(organization.GetMaterials());
            AddNotes(organization.GetNotes());
        }

        private readonly ICollection<HoursOfOperation> hoursOfOperations = 
            new Collection<HoursOfOperation>();

        public virtual void AddHoursOfOperation(
            DayOfWeek dayOfWeek,
            DateTime? openTime,
            DateTime? closeTime,
            DateTime? afterBreakOpenTime,
            DateTime? afterBreakCloseTime,
            bool isClosed = false)
        {
            if (hoursOfOperations.Any(h => h.DayOfWeek == dayOfWeek))
                return;

            if (isClosed)
            {
                openTime = null;
                closeTime = null;
                afterBreakOpenTime = null;
                afterBreakCloseTime = null;
            }

            Guard.Against<BusinessException>(
                !isClosed && (!openTime.HasValue || !closeTime.HasValue),
                "Week day open and close time are required.");

            Guard.Against<BusinessException>(
                !isClosed && openTime.HasValue && closeTime.HasValue && openTime.Value >= closeTime.Value,
                "Week day open and close time range is invalid.");

            Guard.Against<BusinessException>(
                !isClosed &&
                afterBreakOpenTime.HasValue &&
                afterBreakCloseTime.HasValue &&
                (afterBreakOpenTime.Value > afterBreakCloseTime.Value ||
                afterBreakOpenTime.Value <= openTime.Value ||
                afterBreakOpenTime.Value <= closeTime.Value ||
                afterBreakCloseTime.Value <= openTime.Value),
                "Week day after break open and close time range is invalid.");

            hoursOfOperations.Add(new HoursOfOperation(
                this,
                dayOfWeek,
                openTime,
                closeTime,
                afterBreakOpenTime,
                afterBreakCloseTime,
                isClosed));
        }

        public virtual void AddHoursOfOperation(
            string weekDay,
            DateTime? openTime,
            DateTime? closeTime,
            DateTime? afterBreakOpenTime,
            DateTime? afterBreakCloseTime,
            bool isClosed)
        {
            DayOfWeek dayOfWeek;
            Guard.Against<ArgumentException>(
                                    Enum.TryParse(weekDay, out dayOfWeek) == false,
                                    "Week day is not of valid type.");

            AddHoursOfOperation(dayOfWeek, openTime, closeTime, afterBreakOpenTime, afterBreakCloseTime, isClosed);
        }

        public virtual void AddHoursOfOperation(
            IEnumerable<DayOfWeek> weekDays, 
            DateTime? openTime, 
            DateTime? closeTime,
            DateTime? afterBreakOpenTime,
            DateTime? afterBreakCloseTime,
            bool isClosed)
        {
            weekDays.ForEach(weekDay => AddHoursOfOperation(
                                    weekDay, 
                                    openTime, 
                                    closeTime, 
                                    afterBreakOpenTime, 
                                    afterBreakCloseTime,
                                    isClosed));
        }

        private static bool TryParseDateTime(string str, out DateTime? result)
        {
            result = null;
            if (string.IsNullOrWhiteSpace(str))
                return true;

            DateTime date;
            var isSuccess = DateTime.TryParse(str, out date);
            if (isSuccess == false) 
                return false;

            result = date;
            return true;
        }

        private static bool TryParseOpenTime(string str, out DateTime? result)
        {
            return TryParseDateTime(str, out result);
        }

        private static bool TryParseCloseTime(string str, out DateTime? result)
        {
            var isSuccess = TryParseDateTime(str, out result);
            if (!isSuccess) 
                return false;

            if (str == "12:00 AM" && result != null)
                result = result.Value.AddDays(1);

            return true;
        }

        public virtual void AddHoursOfOperation(
            IEnumerable<DayOfWeek> weekDays, 
            string openTime,
            string closeTime,
            string afterBreakOpenTime,
            string afterBreakCloseTime,
            bool isClosed)
        {
            DateTime? openTimeResult;
            DateTime? closeTimeResult;
            DateTime? afterBreakOpenTimeResult;
            DateTime? afterBreakCloseTimeResult;
            
            if(TryParseOpenTime(openTime, out openTimeResult) && 
                TryParseCloseTime(closeTime, out closeTimeResult) && 
                TryParseOpenTime(afterBreakOpenTime, out afterBreakOpenTimeResult) &&
                TryParseCloseTime(afterBreakCloseTime, out afterBreakCloseTimeResult))
            {
                AddHoursOfOperation(
                     weekDays,
                     openTimeResult,
                     closeTimeResult,
                     afterBreakOpenTimeResult,
                     afterBreakCloseTimeResult,
                     isClosed); 
            }
        }

        private void AddHoursOfOperations(IEnumerable<HoursOfOperation> hours)
        {
            hours.ForEach(hour => AddHoursOfOperation(
                hour.DayOfWeek, 
                hour.OpenTime, 
                hour.CloseTime, 
                hour.AfterBreakOpenTime, 
                hour.AfterBreakCloseTime, 
                hour.IsClosed));
        }

        public virtual void TryAddHoursOfOperation(string pattern)
        {
                if (string.IsNullOrEmpty(pattern))
                return;

            new HoursOfOperationParser().Parse(pattern).ForEach(h =>
            {
                h.Organization = this;
                hoursOfOperations.Add(h);
            });
        }

        public virtual void TryUpdateHoursOfOperation(long environmentalId, string pattern, ISession session)
        {
            var existingHours = session.Query<HoursOfOperation>()
                              .Where(h => h.Organization.Id == environmentalId)
                              .ToList();

            var parsedHours = new HoursOfOperationParser().Parse(pattern);

            foreach (var parsed in parsedHours)
            {
                parsed.Organization = this;
                var existing = existingHours.FirstOrDefault(h => h.DayOfWeek == parsed.DayOfWeek);

                if (existing != null)
                {
                    
                    existing.OpenTime = parsed.OpenTime;
                    existing.CloseTime = parsed.CloseTime;
                    existing.IsClosed = parsed.IsClosed;
                    hoursOfOperations.Add(existing);
                  
                }
                else
                {
                    hoursOfOperations.Add(parsed);
                }
            }
        }
        public virtual HoursOfOperation GetHoursOfOperation(int id)
        {
            return hoursOfOperations.FirstOrDefault(h => h.Id == id);
        }

        public virtual bool RemoveHoursOfOperation(HoursOfOperation hoursOfOperation)
        {
            return hoursOfOperations.Contains(hoursOfOperation) && 
                hoursOfOperations.Remove(hoursOfOperation);
        }

        public virtual void RemoveAllHoursOfOperation()
        {
            hoursOfOperations.Clear();
        }

        public virtual bool RemoveHoursOfOperation(int id)
        {
            return RemoveHoursOfOperation(GetHoursOfOperation(id));
        }

        public virtual IEnumerable<HoursOfOperation> GetHoursOfOperations()
        {
            return hoursOfOperations;
        }

        private readonly ICollection<Note> notes = new Collection<Note>();

        public virtual void AddUpdateNote(string text, AccessLevel access)
        {
            if (string.IsNullOrWhiteSpace(text))
                return;
            var existingNote = notes.FirstOrDefault(n => n.AccessLevel == access);

            if (existingNote != null)
            {

                if (!string.Equals(existingNote.Text, text, StringComparison.Ordinal))
                {
                    existingNote.Text = text;
                }
            }
            else
            {
                if (string.IsNullOrEmpty(text)
                || notes.FirstOrDefault(n => n.Text == text && n.AccessLevel == access) != null)
                    return;

                notes.Add(new Note(text, access));
            }
         }

        public virtual void AddNote(string text, string access)
        {
            AccessLevel accessLevel;
            Guard.Against<ArgumentException>(
                Enum.TryParse(access, out accessLevel) == false,
                "Note access level is not of valid type.");

            AddUpdateNote(text, accessLevel);
        }
        private void AddNotes(IEnumerable<Note> organizationNotes)
        {
            organizationNotes.ForEach(note => AddUpdateNote(note.Text, note.AccessLevel));    
        }

        public virtual Note GetNote(int id)
        {
            return notes.FirstOrDefault(n => n.Id == id);
        }

        public virtual bool RemoveNote(Note note) 
        {
            return notes.Contains(note) && notes.Remove(note);
        }

        public virtual bool RemoveNote(int id)
        {
            return RemoveNote(GetNote(id));
        }

        public virtual void RemoveAllNotes()
        {
            notes.Clear();    
        }

        public virtual IEnumerable<Note> GetNotes()
        {
            return notes;
        }

        private readonly ICollection<EnvironmentalOrganizationMaterial> materials = 
            new Collection<EnvironmentalOrganizationMaterial>();

        public virtual void AddMaterial(
            Material material,
            IEnumerable<MaterialDeliveryType> residentialDeliveries,
            IEnumerable<MaterialDeliveryType> businessDeliveries)
        {
            if (materials.FirstOrDefault(m => m.Material.Id == material.Id) != null)
                return;

            Guard.Against<ArgumentNullException>(
                residentialDeliveries == null,
                "Material residential delivery is null");

            Guard.Against<ArgumentNullException>(
                businessDeliveries == null,
                "Material business delivery is null");

            var isAnyResidentialDelivery = residentialDeliveries.Any();
            var isAnyBusinessDelivery = businessDeliveries.Any();

            Guard.Against<BusinessException>(
                !isAnyResidentialDelivery & !isAnyBusinessDelivery,
                string.Format("Material delivery options are required."));

            var organizationMaterial = new EnvironmentalOrganizationMaterial(material, this);

            if (isAnyResidentialDelivery)
                organizationMaterial.AddMaterialDelivery(residentialDeliveries, false);

            if (isAnyBusinessDelivery)
                organizationMaterial.AddMaterialDelivery(businessDeliveries, true);

            materials.Add(organizationMaterial);
        }

        private static IEnumerable<MaterialDeliveryType> ParseDeliveryType(
            IEnumerable<string> deliveries)
        {
            return deliveries.Select(delivery => (MaterialDeliveryType) Enum.Parse(
                                                        typeof (MaterialDeliveryType),
                                                        delivery,
                                                        true));
        }

        public virtual void AddMaterial(
           Material material,
           IEnumerable<string> residentialDeliveries,
           IEnumerable<string> businessDeliveries)
        {
            AddMaterial(
                material, 
                ParseDeliveryType(residentialDeliveries), 
                ParseDeliveryType(businessDeliveries));
        }

        private void AddMaterials(IEnumerable<EnvironmentalOrganizationMaterial> organizationMaterials)
        {
            organizationMaterials.ForEach(m => AddMaterial(
                m.Material, 
                m.GetMaterialDeliveries(false).Select(d => d.DeliveryType), 
                m.GetMaterialDeliveries(true).Select(d => d.DeliveryType)));    
        }

        public virtual IEnumerable<EnvironmentalOrganizationMaterial> GetMaterials()
        {
            return materials.OrderBy(m => m.Material.Name);
        }

        public virtual EnvironmentalOrganizationMaterial GetMaterial(int id)
        {
            return materials.SingleOrDefault(m => m.Id == id);
        }

        public virtual bool RemoveMaterial(EnvironmentalOrganizationMaterial material)
        {
            return materials.Contains(material) && materials.Remove(material);
        }

        public virtual bool RemoveMaterial(int id)
        {
            return RemoveMaterial(GetMaterial(id));
        }

        public virtual void RemoveAllMaterials()
        {
            materials.Clear();
        }

        private readonly ICollection<InformationVerificationGroup> verificationGroups = 
            new Collection<InformationVerificationGroup>(); 
        
        public virtual void AddVerificationGroup(InformationVerificationGroup group)
        {
            if (group != null && verificationGroups.Contains(group) == false)
                verificationGroups.Add(group);
        }

        public virtual bool RemoveVerificationGroup(int id)
        {
            var group = verificationGroups.FirstOrDefault(g => g.Id == id);
            return group != null && verificationGroups.Remove(group);
        }

        public virtual IEnumerable<InformationVerificationGroup> GetVerificationGroups()
        {
            return verificationGroups.OrderBy(g => g.Name);
        }

        /// <summary>
        /// Creates a new instance of the <see cref="System.Object"/> with reference
        /// objects copied as new instances as well.
        /// </summary>
        /// <returns>New instance of the <see cref="System.Object"/></returns>
        public abstract object Copy();

        public virtual void RemoveMaterials(
   Material material,
   IEnumerable<MaterialDeliveryType> residentialDeliveries,
   IEnumerable<MaterialDeliveryType> businessDeliveries)
        {
            var organizationMaterial = materials.FirstOrDefault(m => m.Material.Id == material.Id);

            if (organizationMaterial == null)
                return;

            Guard.Against<ArgumentNullException>(
                residentialDeliveries == null,
                "Material residential delivery is null");

            Guard.Against<ArgumentNullException>(
                businessDeliveries == null,
                "Material business delivery is null");

            var isAnyResidentialDelivery = residentialDeliveries.Any();
            var isAnyBusinessDelivery = businessDeliveries.Any();

            Guard.Against<BusinessException>(
                !isAnyResidentialDelivery & !isAnyBusinessDelivery,
                "At least one delivery option must be selected for removal.");

            if (isAnyResidentialDelivery)
                organizationMaterial.RemoveMaterialsDelivery(residentialDeliveries, false);

            if (isAnyBusinessDelivery)
                organizationMaterial.RemoveMaterialsDelivery(businessDeliveries, true);
            materials.Remove(organizationMaterial);


        }

        public virtual void RemoveMaterials(
           Material material,
           IEnumerable<string> residentialDeliveries,
           IEnumerable<string> businessDeliveries)
        {
            RemoveMaterials(
                material,
                ParseDeliveryType(residentialDeliveries),
                ParseDeliveryType(businessDeliveries));
        }
    }
}