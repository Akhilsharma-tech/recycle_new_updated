using ElectronicRecyclers.One800Recycling.Domain.Common;
using ElectronicRecyclers.One800Recycling.Domain.ValueObjects;
using System.Collections.Generic;

namespace ElectronicRecyclers.One800Recycling.Domain.Entities
{
    public class EnvironmentalOrganizationImport : DomainObject
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

        public virtual string PrivateNote { get; set; }

        public virtual string PublicNote { get; set; }

        public virtual string HoursOfOperation { get; set; }

        public virtual string Type { get; set; }

        public virtual string MaterialResidentialDeliveryOption { get; set; }

        public virtual string MaterialBusinessDeliveryOption { get; set; }

        public virtual bool IsDedicatedRecycler { get; set; }

        public virtual bool IsNameValid { get; set; }

        public virtual bool IsTelephoneValid { get; set; }

        public virtual bool IsFaxValid { get; set; }

        public virtual bool IsAddressValid { get; set; }

        public virtual bool IsWebsiteUrlValid { get; set; }

        public virtual bool IsHoursOfOperationValid { get; set; }

        public virtual bool IsDuplicate { get; set; }

        public virtual bool IsDuplicateOrganizationFoundDuringMoveOperation { get; set; }
        public virtual long? EnvironmentalOrganizationId { get; set; }
        public virtual bool IsEnabled { get; set; }
        

        public virtual bool HasErrors
        {
            get
            {
                return !IsNameValid 
                    || !IsTelephoneValid
                    || !IsFaxValid
                    || !IsAddressValid
                    || !IsWebsiteUrlValid
                    || !IsHoursOfOperationValid
                    || IsDuplicate
                    || IsDuplicateOrganizationFoundDuringMoveOperation;
            }
        }

        public virtual void ClearErrors()
        {
            IsNameValid = true;
            IsTelephoneValid = true;
            IsFaxValid = true;
            IsAddressValid = true;
            IsWebsiteUrlValid = true;
            IsHoursOfOperationValid = true;
            IsDuplicate = false;
            IsDuplicateOrganizationFoundDuringMoveOperation = false;
        }

        public virtual IEnumerable<string> GetErrors()
        {
            if (IsNameValid == false)
                yield return "Name is not valid.";

            if (IsTelephoneValid == false)
                yield return "Phone is not valid.";

            if (IsFaxValid == false)
                yield return "Fax is not valid.";

            if (IsAddressValid == false)
                yield return "Address was not verified.";

            if (IsWebsiteUrlValid == false)
                yield return "Website url is not valid.";

            if (IsHoursOfOperationValid == false)
                yield return "Business hours string is not valid. Check for extra white spaces." +
                             "Valid example: Mon-Fri 8:00-17:00, Sat 10:30-14:00, Sun Closed";

            if (IsDuplicate)
                yield return "A duplicate record was found. The same site already exists in the Recycler Companies list.";

            if (IsDuplicateOrganizationFoundDuringMoveOperation)
                yield return "Duplicate record found in active recycling companies list " +
                             "when attempting to move this row.";
        }
    }
}