using ElectronicRecyclers.One800Recycling.Application.Common;
using ElectronicRecyclers.One800Recycling.Application.ETLProcess.IOperationETL;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicRecyclers.One800Recycling.Application.Import.Processes
{
    public abstract class AbstractEtlProcess
    {
        private readonly List<IOperationETL> operations = new();
        private IHubContext<ProgressBarHub> hubContext;
        private IOperationETL operationWithProgressReporting;
        private const string RowsCountKey = "%rowsCount%";
        protected IProgress<double> Progress { get; private set; }

        protected AbstractEtlProcess(IHubContext<ProgressBarHub> hubContext)
        {
            this.hubContext = hubContext;
        }

        public async Task ExecuteAsync(IEnumerable<DynamicReader> input)
        {
            Initialize();
            var result = await RunOperationsAsync(input);
            PostProcessing(result);
        }

        protected virtual void Initialize()
        {
            Progress = new Progress<double>(percent =>
            {
                hubContext.Clients.All.SendAsync("broadcastProgress", percent);
            });
            Progress.Report(0);
            Thread.Sleep(1000); // Optional delay
        }

        private async Task<IEnumerable<DynamicReader>> RunOperationsAsync(IEnumerable<DynamicReader> input)
        {
            var result = input;
            foreach (var op in GetOperations())
            {
                if (op == operationWithProgressReporting)
                    op.OnRowProcessed += row => ReportProgress(op, row); 

                result = await op.ExecuteAsync(result);
            }
            return result;
        }

        private void ReportProgress(IOperationETL op, Dictionary<string, object> row)
        {
            if (!row.TryGetValue(RowsCountKey, out var totalObj) || totalObj is not int totalRows)
                return;

            var current = 1; // Replace with actual tracking logic if needed
            var progress = Math.Round((double)current / totalRows * 100, 2);
            Progress.Report(progress);
        }

        protected virtual void PostProcessing(IEnumerable<Dictionary<string, object>> result)
        {

        }

       
        protected void Register(IOperationETL op)
        {
            operations.Add(op);
        }

        
        protected void RegisterWithProgressReporting(IOperationETL op)
        {
            if (operationWithProgressReporting != null)
                throw new Exception("Only one operation per ETL process allowed for progress reporting");

            operationWithProgressReporting = op;
            Register(op);
        }

        protected virtual IEnumerable<IOperationETL> GetOperations() => operations;
    }

}
