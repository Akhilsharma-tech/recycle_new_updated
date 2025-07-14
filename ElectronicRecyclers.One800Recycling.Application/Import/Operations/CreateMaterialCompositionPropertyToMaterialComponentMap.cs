using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using ElectronicRecyclers.One800Recycling.Domain.Entities;
using NHibernate;



namespace ElectronicRecyclers.One800Recycling.Application.Import.Operations
{
    public class CreateMaterialCompositionPropertyToMaterialComponentMap 
    {
        private readonly ISession session;

        public CreateMaterialCompositionPropertyToMaterialComponentMap(ISession session)
        {
            this.session = session;
        }

        private static string FormatColumnName(string column)
        {
            column = column.Replace("_", " ");
            column = column.Replace("COMMA", ",");
            column = column.Replace("DASH", "-");
            column = column.Replace("DOT", ".");
            column = column.Replace("FORWARDSLASH", "/");
            column = column.Replace("OPENBRACKET", "(");
            column = column.Replace("CLOSEBRACKET", ")");
            column = column.Replace("ProductDismantling ", "");
            return column.Trim();
        }

        public IEnumerable<Dictionary<string,object>> Execute(IEnumerable<Dictionary<string,object>> rows)
        {
            var materialComponents = session
                .CreateCriteria<MaterialComponent>()
                .List<MaterialComponent>();

            var productProcesses = session
                .CreateCriteria<ProductDismantlingProcess>()
                .List<ProductDismantlingProcess>();

            foreach (var row in rows)
            {
                var components = new List<Tuple<MaterialComponent, string>>();
                var processes = new List<Tuple<ProductDismantlingProcess, string>>();
                row["HasError"] = false;

                foreach (var column in row.Columns)
                {
                    var value = row[column];
                    if (column == "Name" 
                        || value == null 
                        || string.IsNullOrWhiteSpace(value.ToString()))
                        continue;

                    var componentOrProcessName = FormatColumnName(column);

                    if (Regex.IsMatch(column, "^ProductDismantling_"))
                    {
                        var process = productProcesses
                            .FirstOrDefault(p => p.ProductName.Equals(
                                componentOrProcessName, 
                                StringComparison.CurrentCultureIgnoreCase));

                        if (process == null)
                        {
                            row["HasError"] = true;
                            continue;
                        }
                        
                        processes.Add(new Tuple<ProductDismantlingProcess, string>(
                            process,
                            value.ToString()));
                    }
                    else
                    {
                        var component = materialComponents
                            .OrderBy(c => c.Name)
                            .FirstOrDefault(c => c.Name.Trim().Equals(
                                componentOrProcessName, 
                                StringComparison.CurrentCultureIgnoreCase));

                        if (component == null)
                        {
                            row["HasError"] = true;
                            continue;
                        }

                        components.Add(new Tuple<MaterialComponent, string>(
                            component,
                            value.ToString()));
                    }
                }

                row["MaterialComponents"] = components;
                row["ProductDismantlingProcesses"] = processes;
                yield return row;
            }
        }
    }
}