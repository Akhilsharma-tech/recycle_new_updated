using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Linq;
using System.Text.RegularExpressions;
using ElectronicRecyclers.One800Recycling.Application.Common;
using ElectronicRecyclers.One800Recycling.Application.ETLProcess;
using ElectronicRecyclers.One800Recycling.Application.Logging;
using ElectronicRecyclers.One800Recycling.Domain.Common;
using ElectronicRecyclers.One800Recycling.Domain.ValueObjects;
using NHibernate;
using NHibernate.Criterion;



namespace ElectronicRecyclers.One800Recycling.Application.Import.Operations
{
    public class CheckOrganizationsForDuplicates<T> : AbstractOperation
    where T : DomainObject
    {
        private readonly string column;
        public CheckOrganizationsForDuplicates(string column)
        {
            this.column = column;
        }

        public override IEnumerable<DynamicReader> Execute(IEnumerable<DynamicReader> rows)
        {
            LogManager.GetLogger().Info("Check organization imports for duplicates");

            using (var session = NHSessionProvider.OpenSession())
            using (var transaction = session.BeginTransaction())
            {
                var multiCriteria = session.CreateMultiCriteria();
                var rowsList = rows.ToList();
                const int batchSize = 250;
                var results = new List<DynamicReader>();

                for (var i = 0; i <= rowsList.Count(); i += batchSize)
                {
                    var criteriaRows = new List<DynamicReader>();
                    foreach (var row in rowsList.Skip(i).Take(batchSize).ToList())
                    {
                        object idValue = row["EnvironmentalOrganizationId"];
                        bool isUpdate = idValue != null &&
                                        long.TryParse(idValue.ToString(), out var orgId) &&
                                        orgId > 0;

                        // If it's an update, skip the duplicate check
                        if (isUpdate)
                        {
                            results.Add(row);
                            continue;
                        }

                        var address = (Address)row["Address"];
                        var name = row["Name"].ToString();

                        // Only search for first word in name and street number in address to cast wide net
                        string namePart = name.Split(' ')[0];
                        string addressPart = address.AddressLine1.Split(' ')[0];

                        multiCriteria.Add(
                            row["Id"].ToString(),
                            session.CreateCriteria<T>()
                                .Add(Restrictions.InsensitiveLike("Name", namePart, MatchMode.Anywhere))
                                .Add(Restrictions.InsensitiveLike("Address.AddressLine1", addressPart, MatchMode.Anywhere))
                                .Add(Restrictions.Eq("Address.PostalCode", address.PostalCode))
                                .SetProjection(Projections.Property("Id"))
                                .SetMaxResults(10)
                        );


                        criteriaRows.Add(row);
                    }

                    foreach (var row in criteriaRows)
                    {
                        var multiResults = (IList)multiCriteria.GetResult(row["Id"].ToString());
                        var inputName = Normalize(row["Name"].ToString());
                        var inputAddress = Normalize(((Address)row["Address"]).AddressLine1);

                        foreach (var matchedId in multiResults)
                        {
                            var match = session.Get<T>(matchedId);
                            if (match == null) continue;

                            var nameProp = typeof(T).GetProperty("Name");
                            var addressProp = typeof(T).GetProperty("Address");
                            if (nameProp == null || addressProp == null) continue;

                            var dbName = Normalize(nameProp.GetValue(match)?.ToString());

                            var addressObj = addressProp.GetValue(match);
                            var addressLine1Prop = addressObj?.GetType().GetProperty("AddressLine1");
                            var dbAddress = Normalize(addressLine1Prop?.GetValue(addressObj)?.ToString());

                            //if (multiResults.Count > 0 && inputName == dbName && inputAddress == dbAddress)
                            //{
                            //    row[column] = true;
                            //}
                            if (LevenshteinDistance(inputName, dbName) <= 2 && inputAddress == dbAddress && multiResults.Count > 0)
                            {
                                row[column] = true;
                                break;
                            }
                        }
                        //if (multiResults.Count > 0)
                        //    row[column] = true;

                        results.Add(row);
                    }

                    multiCriteria = session.CreateMultiCriteria();

                    var count = results.Count;
                    if (count % 1000 == 0)
                        LogManager.GetLogger().Info("Check organizations for duplicates in progress: " + count);
                }

                transaction.Commit();
                return results;
            }
        }

        public static string Normalize(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return string.Empty;

            input = input.ToLowerInvariant().Trim();
            input = Regex.Replace(input, @"[^a-z0-9\s]", " ");
            input = Regex.Replace(input, @"\s+", " ");
            return input.Trim();
        }

        private static int LevenshteinDistance(string a, string b)
        {
            if (string.IsNullOrEmpty(a)) return b?.Length ?? 0;
            if (string.IsNullOrEmpty(b)) return a.Length;

            var lenA = a.Length;
            var lenB = b.Length;
            var matrix = new int[lenA + 1, lenB + 1];

            for (int i = 0; i <= lenA; i++) matrix[i, 0] = i;
            for (int j = 0; j <= lenB; j++) matrix[0, j] = j;

            for (int i = 1; i <= lenA; i++)
            {
                for (int j = 1; j <= lenB; j++)
                {
                    int cost = a[i - 1] == b[j - 1] ? 0 : 1;
                    matrix[i, j] = Math.Min(
                        Math.Min(matrix[i - 1, j] + 1, matrix[i, j - 1] + 1),
                        matrix[i - 1, j - 1] + cost);
                }
            }

            return matrix[lenA, lenB];
        }
    }

    //public class CheckOrganizationsForDuplicates<T> 
    //    where T : DomainObject
    //{
    //    private readonly string column;
    //    public CheckOrganizationsForDuplicates(string column)
    //    {
    //        this.column = column;
    //    }

    //    public override IEnumerable<Dictionary<string,object>> Execute(IEnumerable<Dictionary<string,object>> rows)
    //    {
    //        LogManager.GetLogger().Info("Check organization imports for duplicates");

    //        using (var session = NHSessionProvider.OpenSession())
    //        using (var transaction = session.BeginTransaction())
    //        {
    //            var multiCriteria = session.CreateMultiCriteria();
    //            var rowsList = rows.ToList();
    //            const int batchSize = 250;
    //            var results = new List<Row>();

    //            for (var i = 0; i <= rowsList.Count(); i += batchSize)
    //            {
    //                var criteriaRows = new List<Row>();
    //                foreach (var row in rowsList.Skip(i).Take(batchSize).ToList())
    //                {                         
    //                    object idValue = row["EnvironmentalOrganizationId"];
    //                    bool isUpdate = idValue != null &&
    //                                    long.TryParse(idValue.ToString(), out var orgId) &&
    //                                    orgId > 0;

    //                    // If it's an update, skip the duplicate check
    //                    if (isUpdate)
    //                    {
    //                        results.Add(row);
    //                        continue;
    //                    }

    //                    var address = (Address)row["Address"];
    //                    var name = row["Name"].ToString();

    //                    // Only search for first word in name and street number in address to cast wide net
    //                    string namePart = name.Split(' ')[0];         
    //                    string addressPart = address.AddressLine1.Split(' ')[0];

    //                    multiCriteria.Add(
    //                        row["Id"].ToString(),
    //                        session.CreateCriteria<T>()
    //                            .Add(Restrictions.InsensitiveLike("Name", namePart, MatchMode.Anywhere))
    //                            .Add(Restrictions.InsensitiveLike("Address.AddressLine1", addressPart, MatchMode.Anywhere))
    //                            .Add(Restrictions.Eq("Address.PostalCode", address.PostalCode))
    //                            .SetProjection(Projections.Property("Id"))
    //                            .SetMaxResults(10)
    //                    );


    //                    criteriaRows.Add(row);
    //                }

    //                foreach (var row in criteriaRows)
    //                {
    //                    var multiResults = (IList)multiCriteria.GetResult(row["Id"].ToString());
    //                    var inputName = Normalize(row["Name"].ToString());
    //                    var inputAddress = Normalize(((Address)row["Address"]).AddressLine1);

    //                    foreach (var matchedId in multiResults)
    //                    {
    //                        var match = session.Get<T>(matchedId);
    //                        if (match == null) continue;

    //                        var nameProp = typeof(T).GetProperty("Name");
    //                        var addressProp = typeof(T).GetProperty("Address");
    //                        if (nameProp == null || addressProp == null) continue;

    //                        var dbName = Normalize(nameProp.GetValue(match)?.ToString());

    //                        var addressObj = addressProp.GetValue(match);
    //                        var addressLine1Prop = addressObj?.GetType().GetProperty("AddressLine1");
    //                        var dbAddress = Normalize(addressLine1Prop?.GetValue(addressObj)?.ToString());

    //                        //if (multiResults.Count > 0 && inputName == dbName && inputAddress == dbAddress)
    //                        //{
    //                        //    row[column] = true;
    //                        //}
    //                        if (LevenshteinDistance(inputName, dbName) <= 2 && inputAddress == dbAddress && multiResults.Count > 0 )
    //                        {
    //                            row[column] = true;
    //                            break;
    //                        }
    //                    }
    //                    //if (multiResults.Count > 0)
    //                    //    row[column] = true;

    //                    results.Add(row);
    //                }

    //                multiCriteria = session.CreateMultiCriteria();

    //                var count = results.Count;
    //                if (count % 1000 == 0)
    //                    LogManager.GetLogger().Info("Check organizations for duplicates in progress: "+count);
    //            }

    //            transaction.Commit();
    //            return results;
    //        }
    //    }

    //    public static string Normalize(string input)
    //    {
    //        if (string.IsNullOrWhiteSpace(input))
    //            return string.Empty;

    //        input = input.ToLowerInvariant().Trim();
    //        input = Regex.Replace(input, @"[^a-z0-9\s]", " "); 
    //        input = Regex.Replace(input, @"\s+", " ");         
    //        return input.Trim();
    //    }

    //    private static int LevenshteinDistance(string a, string b)
    //    {
    //        if (string.IsNullOrEmpty(a)) return b?.Length ?? 0;
    //        if (string.IsNullOrEmpty(b)) return a.Length;

    //        var lenA = a.Length;
    //        var lenB = b.Length;
    //        var matrix = new int[lenA + 1, lenB + 1];

    //        for (int i = 0; i <= lenA; i++) matrix[i, 0] = i;
    //        for (int j = 0; j <= lenB; j++) matrix[0, j] = j;

    //        for (int i = 1; i <= lenA; i++)
    //        {
    //            for (int j = 1; j <= lenB; j++)
    //            {
    //                int cost = a[i - 1] == b[j - 1] ? 0 : 1;
    //                matrix[i, j] = Math.Min(
    //                    Math.Min(matrix[i - 1, j] + 1, matrix[i, j - 1] + 1),
    //                    matrix[i - 1, j - 1] + cost);
    //            }
    //        }

    //        return matrix[lenA, lenB];
    //    }
    //}
}