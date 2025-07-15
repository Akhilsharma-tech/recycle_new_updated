using ElectronicRecyclers.One800Recycling.Domain.ValueObjects;
using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicRecyclers.One800Recycling.Application.Common
{

    public abstract class EtlPipeline<TDerived> where TDerived : EtlPipeline<TDerived>
    {
        protected readonly List<IEtlOperation> _operations = new();
        public bool UseTransaction { get; set; }
        public string Name { get; protected set; } = typeof(TDerived).Name;

        protected void Debug(string message, params object[] args)
        {
            Console.WriteLine(message, args); // or use ILogger
        }

        public TDerived Register(IEtlOperation operation)
        {
            operation.UseTransaction = UseTransaction;
            _operations.Add(operation);
            Debug("Register {0} in {1}", operation.Name, Name);
            return (TDerived)this;
        }

        public async Task ExecuteAsync(CancellationToken cancellationToken = default)
        {
            foreach (var operation in _operations)
            {
                await operation.ExecuteAsync(cancellationToken);
            }
        }
    }

    public interface IEtlOperation
    {
        string Name { get; }
        bool UseTransaction { get; set; }
        Task ExecuteAsync(CancellationToken cancellationToken);
    }

}
