using ElectronicRecyclers.One800Recycling.Application.Common;
using ElectronicRecyclers.One800Recycling.Application.ETLProcess;
using ElectronicRecyclers.One800Recycling.Application.Import.Operations;
using ElectronicRecyclers.One800Recycling.Application.Logging;
using Microsoft.AspNetCore.SignalR;
using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ElectronicRecyclers.One800Recycling.Application.Import.Processes
{
    public abstract class AbstractEtlProcess : EtlProcess
    {
        private readonly IHubContext<ProgressBarHub> _hubContext;
        private IOperationETL operationWithProgressReporting;
        private const string RowsCountKey = "%rowsCount%";

        private static void PauseTaskForJustAMomentToLetUserInterfaceToLoad()
        {
            Thread.Sleep(4000);//It is done for task progress reporting to the ui. -Roman
        }

        protected override void Initialize()
        {
            PauseTaskForJustAMomentToLetUserInterfaceToLoad();

            Progress = new Progress<double>(percent =>
            {
                _hubContext.Clients.All.SendAsync("broadcastProgress", percent);
            });
            Progress.Report(0); 
        }

        private static double CalculateProgress(long current, int total)
        {
            var progress = (current >= total) ? 100.00 : ((double)current / (double)total) * 100;
            return Math.Round(progress, 2);
        }

        private void ReportProgress(IOperationETL op, DynamicReader dictionary)
        {
            var rowsCount = 0;
            if (dictionary.ContainsKey(RowsCountKey) && dictionary[RowsCountKey] != null)
                rowsCount = (int)dictionary[RowsCountKey];

            var current = op.Statistics.OutputtedRows;
            var progress = CalculateProgress(current, rowsCount);

            Progress.Report(progress);
            Thread.Sleep(300); //Give time for ui to update progress bar. -Roman
        }

        protected IProgress<double> Progress { get; private set; }

        protected override void PostProcessing()
        {
            GetAllErrors().ForEach(e =>
            {

                _hubContext.Clients.All.SendAsync("broadcastError", e.Message);
                LogManager.GetLogger().Error(e.Message, e);
            });
        }

        protected void RegisterWithProgressReporting(IOperationETL op)
        {
            if (operationWithProgressReporting != null)
                throw new Exception("Only one operation per etl process allowed to have progress reporting capability.");

            operationWithProgressReporting = op;
            operationWithProgressReporting.OnRowProcessed += ReportProgress;

            Register(new CountRows());
            Register(operationWithProgressReporting);
        }
    }

}
