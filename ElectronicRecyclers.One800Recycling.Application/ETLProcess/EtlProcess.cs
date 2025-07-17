using ElectronicRecyclers.One800Recycling.Application.ETLProcess;
using ElectronicRecyclers.One800Recycling.Domain.ValueObjects;
using Microsoft.AspNetCore.JsonPatch.Operations;
using Microsoft.CodeAnalysis;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicRecyclers.One800Recycling.Application
{
    public abstract class EtlProcess
    {
        private readonly List<Exception> _errors = new();
        private readonly List<EtlProcess> _childProcesses = new();
        private readonly List<IOperationETL> _operations = new();
        private readonly ILogger<EtlProcess>? _logger;

        public bool UseTransaction { get; set; } = true;

        public EtlProcess(ILogger<EtlProcess>? logger = null)
        {
            _logger = logger;
        }

        public EtlProcess Register(IOperationETL operation)
        {
            if (operation == null) throw new ArgumentNullException(nameof(operation));

            operation.UseTransaction = this.UseTransaction;
            _operations.Add(operation);

            _logger?.LogDebug("Registered operation {OperationName} in process {ProcessName}",
                              operation.Name, GetType().Name);

            return this;
        }

        public IEnumerable<Exception> GetAllErrors()
        {
            foreach (var error in _errors)
                yield return error;

            foreach (var child in _childProcesses)
            {
                foreach (var childError in child.GetAllErrors())
                    yield return childError;
            }

            foreach (var op in _operations)
            {
                foreach (var opError in op.GetAllErrors())
                    yield return opError;
            }
        }

        public void AddError(Exception ex)
        {
            ArgumentNullException.ThrowIfNull(ex);
            _errors.Add(ex);
        }

        public void AddChildProcess(EtlProcess process)
        {
            ArgumentNullException.ThrowIfNull(process);
            _childProcesses.Add(process);
        }

        protected abstract void Initialize();

        public void Execute()
        {
            Initialize();
            PostProcessing();
        }
        protected virtual void PostProcessing()
        {
        }
    }

}
