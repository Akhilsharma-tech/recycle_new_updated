using System.Collections.Generic;
using System.Text;
using ElectronicRecyclers.One800Recycling.Application.Common;
using ElectronicRecyclers.One800Recycling.Application.ETLProcess;
using ElectronicRecyclers.One800Recycling.Application.Import.Records;
using FileHelpers;
using NHibernate.Impl;




namespace ElectronicRecyclers.One800Recycling.Application.Import.Operations
{
    public class SaveMaterialsToFile : AbstractOperation
    {
        private readonly string filePath;

        public SaveMaterialsToFile(string filePath)
        {
            this.filePath = filePath;
        }

        public override IEnumerable<DynamicReader> Execute(IEnumerable<DynamicReader> rows)
        {
            var engine = new FileHelperEngine<MaterialEnvironmentalImpactRecord>();
            engine.HeaderText = "Material Name" +
                                "\tClimate Change Impact Virgin Production Process" +
                                "\tClimate Change Impact Product Dismantling Process" +
                                "\tClimate Change Impact Recycling Process" +
                                "\tClimate Change Impact Landfilling Process" +
                                "\tClimate Change Impact Incineration Process" +
                                "\tClimate Change Net Impact" +
                                "\tResource Depletion Impact Virgin Production Process" +
                                "\tResource Depletion Impact Product Dismantling Process" +
                                "\tResource Depletion Impact Recycling Process" +
                                "\tResource Depletion Impact Landfilling Process" +
                                "\tResource Depletion Impact Incineration Process" +
                                "\tResource Depletion Net Impact" +
                                "\tWater Withdrawal Impact Virgin Production Process" +
                                "\tWater Withdrawal Impact Product Dismantling Process" +
                                "\tWater Withdrawal Impact Recycling Process" +
                                "\tWater Withdrawal Impact Landfilling Process" +
                                "\tWater Withdrawal Impact Incineration Process" +
                                "\tWater Withdrawal Net Impact" +
                                "\tCar distance avoided (mi)" +
                                "\tGallon of oil avoided" + 
                                "\tGallon of water avoided";

            using (var file = new StreamWriter(filePath, false, Encoding.UTF8))
            {
                foreach (var row in rows)
                {
                    var record = row.ToObject<MaterialEnvironmentalImpactRecord>();

                    string line = engine.WriteString(new[] { record });
                    file.Write(line);
                }    
            }

            yield break;
        }
    }
}