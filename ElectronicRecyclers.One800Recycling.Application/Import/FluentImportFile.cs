using System;
using System.IO;
using System.Text;
using FileHelpers;


namespace ElectronicRecyclers.One800Recycling.Application.Import
{
    public class FluentImportFile
    {
        private readonly FileHelperAsyncEngine engine;

        public static FluentImportFile For<T>()
        {
            return new FluentImportFile(typeof(T));
        }

        public FluentImportFile(Type type)
        {
            engine = new FileHelperAsyncEngine(type);
        }

        public FileEngine From(Stream stream)
        {
            engine.BeginReadStream(new StreamReader(stream, engine.Encoding));
            return new FileEngine(engine);
        }

        public Encoding Enconding
        {
            get { return engine.Encoding; }
            set { engine.Encoding = value; }
        }
    }
}