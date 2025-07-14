using System.Collections.Generic;
using System.IO;



namespace ElectronicRecyclers.One800Recycling.Application.Import.Operations
{
    public class ReadFromFile<T> 
    {
        private readonly Stream stream;

        public ReadFromFile(Stream stream)
        {
            this.stream = stream;
        } 

        public override IEnumerable<Dictionary<string,object>> Execute(IEnumerable<Dictionary<string,object>> rows)
        {
            using (var file = FluentImportFile.For<T>().From(stream))
            {
                foreach (var obj in file)
                {
                    yield return Row.FromObject(obj);
                }
            }
        }
    }
}