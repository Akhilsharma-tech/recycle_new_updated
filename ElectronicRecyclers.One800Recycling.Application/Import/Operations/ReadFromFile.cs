using ElectronicRecyclers.One800Recycling.Application.Common;
using ElectronicRecyclers.One800Recycling.Application.ETLProcess;
using System.Collections.Generic;
using System.IO;



namespace ElectronicRecyclers.One800Recycling.Application.Import.Operations
{
    public class ReadFromFile<T> : AbstractOperation
    {
        private readonly Stream stream;

        public ReadFromFile(Stream stream)
        {
            this.stream = stream;
        } 

        public override IEnumerable<DynamicReader> Execute(IEnumerable<DynamicReader> rows)
        {
            using (var file = FluentImportFile.For<T>().From(stream))
            {
                foreach (var obj in file)
                {
                    yield return DynamicReader.FromObject(obj);
                }
            }
        }
    }
}