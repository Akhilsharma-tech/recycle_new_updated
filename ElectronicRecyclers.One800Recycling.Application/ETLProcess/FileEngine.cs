using FileHelpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicRecyclers.One800Recycling.Application.ETLProcess
{
    public class FileEngine : IDisposable, IEnumerable
    {
        private readonly FileHelperAsyncEngine engine;


        public bool HasErrors => engine.ErrorManager.HasErrors;
        public FileEngine(FileHelperAsyncEngine engine)
        {
            this.engine = engine;
        }


        public void Write(object t)
        {
            engine.WriteNext(t);
        }

        public FileEngine OnError(ErrorMode errorMode)
        {
            engine.ErrorManager.ErrorMode = errorMode;
            return this;
        }

        public void OutputErrors(string file)
        {
            engine.ErrorManager.SaveErrors(file);
        }

        public void Dispose()
        {
            IDisposable disposable = engine;
            disposable.Dispose();
        }

        public IEnumerator GetEnumerator()
        {
            IEnumerable enumerable = engine;
            return enumerable.GetEnumerator();
        }
    }
}
