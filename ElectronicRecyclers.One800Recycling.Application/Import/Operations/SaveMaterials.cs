using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;


using NHibernate;



namespace ElectronicRecyclers.One800Recycling.Application.Import.Operations
{
    public class SaveMaterials 
    {
        private readonly ISession session;
        public SaveMaterials(ISession session)
        {
            this.session = session;
        }

        private static void SaveCommaDelimitedString(object rowValue, Action<string> action)
        {
            if (rowValue == null || string.IsNullOrEmpty(rowValue.ToString()))
                return;

            Regex.Replace(rowValue.ToString(), @"[^\w\,]", "")
                .Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries)
                .ForEach(action);
        }

        public override IEnumerable<Dictionary<string,object>> Execute(IEnumerable<Dictionary<string,object>> rows)
        {
            using (session)
            using (var transaction = session.BeginTransaction())
            {
                var categories = session.QueryOver<MaterialCategory>().List();

                foreach (var row in rows.Where(row => !(bool)row["IsDuplicate"]))
                {
                    var material = new Material(
                        row["Name"].ToString(),
                        row["Description"].ToString(),
                        (row["IsActive"] == null) || bool.Parse(row["IsActive"].ToString()));

                    SaveCommaDelimitedString(row["SearchKeywords"], material.AddSearchKeyword);

                    SaveCommaDelimitedString(
                        row["Categories"],
                        categoryName => material.AddCategory(categories
                                            .FirstOrDefault(c => string.Equals(
                                                c.Name, 
                                                categoryName, 
                                                StringComparison.CurrentCultureIgnoreCase))));

                    session.Save(material);
                }

                if (transaction.IsActive)
                    transaction.Commit();

                yield break;    
            }
        }
    }
}