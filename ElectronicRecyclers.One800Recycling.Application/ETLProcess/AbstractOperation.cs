using ElectronicRecyclers.One800Recycling.Application.Common;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicRecyclers.One800Recycling.Application.ETLProcess
{
    public abstract class AbstractOperation : IOperationETL, IDisposable
    {
        private readonly OperationStatistics _statistics = new();
        private readonly List<Exception> _errors = new();
        private bool _useTransaction = true;
        private IPipelineExecuter? _pipelineExecuter;

        public virtual string Name => GetType().Name;

        public bool UseTransaction
        {
            get => _useTransaction;
            set => _useTransaction = value;
        }

        public OperationStatistics Statistics => _statistics;

        protected IPipelineExecuter? PipelineExecuter => _pipelineExecuter;

        public virtual event Action<IOperationETL, DynamicReader>? OnRowProcessed;
        public virtual event Action<IOperationETL>? OnFinishedProcessing;

    

        public virtual void PrepareForExecution(IPipelineExecuter pipelineExecuter)
        {
            _pipelineExecuter = pipelineExecuter;
            _statistics.MarkStarted();
        }

        public void RaiseRowProcessed(DynamicReader row)
        {
            _statistics.MarkRowProcessed();
            OnRowProcessed?.Invoke(this, row);
        }

        public void RaiseFinishedProcessing()
        {
            _statistics.MarkFinished();
            OnFinishedProcessing?.Invoke(this);
        }

        public virtual IEnumerable<Exception> GetAllErrors()
        {
            foreach (var ex in _errors)
                yield return ex;

            if (_pipelineExecuter != null)
            {
                foreach (var ex in _pipelineExecuter.GetAllErrors())
                    yield return ex;
            }
        }

        protected void AddError(Exception ex)
        {
            _errors.Add(ex);
        }

        public abstract IEnumerable<DynamicReader> Execute(IEnumerable<DynamicReader> rows);

        public virtual void Dispose()
        {
            // Clean up if needed
        }


    }
}
