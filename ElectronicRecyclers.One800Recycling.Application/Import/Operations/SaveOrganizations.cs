using System;
using System.Collections.Generic;
using System.Linq;
using ElectronicRecyclers.One800Recycling.Application.Logging;
using NHibernate;
using NHibernate.Linq;
using static System.Collections.Specialized.BitVector32;
using System.Text.RegularExpressions;
using ElectronicRecyclers.One800Recycling.Domain.Entities;
using ElectronicRecyclers.One800Recycling.Domain.Common;
using ElectronicRecyclers.One800Recycling.Domain.Exceptions;
using ElectronicRecyclers.One800Recycling.Application.Common;


namespace ElectronicRecyclers.One800Recycling.Application.Import.Operations
{
    public class SaveOrganizations<T>  where T : EnvironmentalOrganization
    {
       

        private static void AddNotes(EnvironmentalOrganization organization, DynamicReader row)
        {
            try
            {
              organization.AddUpdateNote(row["PrivateNote"] as string, AccessLevel.Private);
              organization.AddUpdateNote(row["PublicNote"] as string, AccessLevel.Public);
            }
            catch (BusinessException ex)
            {
              LogManager.GetLogger().Error(string.Format("Organization: {0}. Message: {1}", row["Name"], ex.Message));
            }
         }
        public static string NormalizeHours(string input)
        {
            var dayMap = new Dictionary<string, string>
            {
                 { "Monday", "Mon" }, { "Tuesday", "Tue" }, { "Wednesday", "Wed" },
                 { "Thursday", "Thu" }, { "Friday", "Fri" }, { "Saturday", "Sat" }, { "Sunday", "Sun" },
                 { "M", "Mon" }, { "Tu", "Tue" }, { "W", "Wed" }, { "Th", "Thu" },
                 { "F", "Fri" }, { "Sa", "Sat" }, { "Su", "Sun" }
            };
            input = Regex.Replace(input, @"(\d{1,2})\s*[:]\s*(\d{1,2})\s*([APMapm]{2})", "$1:$2$3");
            input = Regex.Replace(input, @"\s+", " ");
            var segments = Regex.Split(input, @"\s*,\s*");
            var result = new List<string>();

            foreach (var seg in segments)
            {
                var match = Regex.Match(seg, @"(?<day>[A-Za-z\- ]+)\s+(?<start>\d{1,2}[:.]?\d{0,2}\s*[APMapm]{0,2}|Closed)\s*[-–]?\s*(?<end>\d{1,2}[:.]?\d{0,2}\s*[APMapm]{0,2}|Closed)?", RegexOptions.IgnoreCase);
                if (!match.Success) continue;

                var day = match.Groups["day"].Value.Trim();
                var normalizedDay = string.Join("-", day.Split('-').Select(d => dayMap.ContainsKey(d.Trim()) ? dayMap[d.Trim()] : d.Trim()));

                var start = match.Groups["start"].Value;
                var end = match.Groups["end"].Success ? match.Groups["end"].Value : "";

                var hours = string.IsNullOrWhiteSpace(end) || start.ToLower().Contains("closed") || end.ToLower().Contains("closed")
                    ? "Closed"
                    : $"{To24(start)}-{To24(end)}";

                result.Add($"{normalizedDay} {hours}");
            }

            return string.Join(", ", result);
        }

        private static string To24(string t)
        {
            return DateTime.TryParse(t.Trim(), out var dt) ? dt.ToString("HH:mm") : t.Trim();
        }


        private static void AddHoursOfOperation(EnvironmentalOrganization organization, DynamicReader row)
        {

            ISession session;
            using (session = NHSessionProvider.OpenSession())
            try
            {

                var environmentalId = row["EnvironmentalOrganizationId"] as long?;
                    var newOrganization = row.ToObject<T>();
                    var rawHours = row["HoursOfOperation"] as string;
                    var normalizedHours = NormalizeHours(rawHours);
                    var organizationExist = session.Query<T>()
                                         .FirstOrDefault(x => x.Id == environmentalId);
                    if (organizationExist == null)
                    {
                        organization.TryAddHoursOfOperation(normalizedHours);
                    }
                    else {
                        organization.TryUpdateHoursOfOperation((long)row["EnvironmentalOrganizationId"], normalizedHours, session);

                    }
            }
            catch (BusinessException ex)
            {
                LogManager.GetLogger().Error(
                           string.Format("Organization: {0}. Message: {1}", row["Name"], ex.Message));    
            }
        }

        private static readonly IDictionary<string , Dictionary<Material, IList<string>>> cachedMaterialDeliveries = 
            new Dictionary<string, Dictionary<Material, IList<string>>>();

        private static Dictionary<Material, IList<string>> ParseMaterials(
            object deliveries,
            IEnumerable<Material> materials)
        {
            var results = new Dictionary<Material, IList<string>>();

            if (deliveries == null)
                return results;

            var materialDeliveries = deliveries.ToString();

            if (cachedMaterialDeliveries.ContainsKey(materialDeliveries))
                return cachedMaterialDeliveries[materialDeliveries];

            materialDeliveries 
                .Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries)
                .ForEach(o =>
                {
                    var values = o.Split(new[] { "|" }, StringSplitOptions.RemoveEmptyEntries);
                    if (values.Count() != 2)
                        return;

                    var material = materials.FirstOrDefault(m => m.Id.ToString() == values[0].ToString());
                    if (material == null)
                        return;

                    var deliveryType = values[1];

                    if (results.ContainsKey(material))
                        results[material].Add(deliveryType);
                    else
                        results.Add(material, new List<string>{deliveryType});
                });

            cachedMaterialDeliveries.Add(materialDeliveries, results);
            return results;
        }

        private static void AddMaterials(
            EnvironmentalOrganization organization, 
            IList<Material> materials, 
            DynamicReader row)
        {
            var parsedMaterials = new Dictionary<Material, Tuple<IList<string>, IList<string>>>(); 
 
            ParseMaterials(row["MaterialResidentialDeliveryOption"], materials).ForEach(m => 
                parsedMaterials.Add(
                    m.Key, 
                    new Tuple<IList<string>, IList<string>>(m.Value, new List<string>())));

            ParseMaterials(row["MaterialBusinessDeliveryOption"], materials).ForEach(m =>
            {
                if (parsedMaterials.ContainsKey(m.Key))
                {
                    parsedMaterials[m.Key] = new Tuple<IList<string>, IList<string>>(
                        parsedMaterials[m.Key].Item1,
                        m.Value);
                }
                else
                {
                    parsedMaterials.Add(
                    m.Key,
                    new Tuple<IList<string>, IList<string>>(new List<string>(), m.Value));
                }
            });

            parsedMaterials.ForEach(m => organization.AddMaterial(
                m.Key,
                m.Value.Item1,
                m.Value.Item2));
        }

        public  IEnumerable<DynamicReader> Execute(IEnumerable<DynamicReader> rows)
        {
            LogManager.GetLogger().Info("Begin saving organizations into the active list");

            ISession session;
            ITransaction transaction;

            using (session = NHSessionProvider.OpenSession())
            using (transaction = session.BeginTransaction())
            {
                var materials = session
                     .CreateCriteria<Material>()
                     .List<Material>();

                const int transactionCommitSize = 500;
                var count = 0;

                foreach (var row in rows)
                {
                    if (!(bool)row["IsDuplicate"] &&
                        !(bool)row["IsDuplicateOrganizationFoundDuringMoveOperation"])
                    {
                        var environmentalId = row["EnvironmentalOrganizationId"] as long? ?? 0;
                        var newOrganization = row.ToObject<T>();
                        var organizationExist = session.Query<T>()
                                                 .FirstOrDefault(x => x.Id == environmentalId);

                        T organization;

                        if (organizationExist != null)
                        {
                            organization = organizationExist;

                            foreach (var prop in typeof(T).GetProperties())
                            {
                                if (prop.CanRead && prop.CanWrite && prop.Name != "Id")
                                {
                                    var value = prop.GetValue(newOrganization, null);
                                    prop.SetValue(organization, value, null);
                                }
                            }

                            session.Update(organization);
                        }
                        else
                        {
                            organization = newOrganization;
                            session.Save(organization);
                        }
                        AddNotes(organization, row);
                        AddHoursOfOperation(organization, row);
                        AddMaterials(organization, materials, row);

                        session.SaveOrUpdate(organization);
                        session.Delete(row.ToObject<EnvironmentalOrganizationImport>());

                        if (++count % transactionCommitSize == 0 && transaction.IsActive)
                        {
                            transaction.Commit();
                            session.Close();
                            session = NHSessionProvider.OpenSession();
                            transaction = session.BeginTransaction();

                            LogManager
                                .GetLogger()
                                .Info("Save organizations operation in progress: " + count);
                        }
                    }
                }

                if (transaction.IsActive)
                    transaction.Commit();

                LogManager
                    .GetLogger()
                    .Info(count + " organization(s) were moved to active list.");

                yield break;
            }
        }
    }
}