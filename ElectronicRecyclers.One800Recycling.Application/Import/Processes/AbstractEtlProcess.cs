using System;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Threading;
using ElectronicRecyclers.One800Recycling.Application.Common;
using ElectronicRecyclers.One800Recycling.Application.Import.Operations;
using ElectronicRecyclers.One800Recycling.Application.Logging;
using ElectronicRecyclers.One800Recycling.Infrastructure.Hubs;
using Microsoft.AspNetCore.SignalR;
using Microsoft.CodeAnalysis;




namespace ElectronicRecyclers.One800Recycling.Application.Import.Processes
{
    public abstract class AbstractEtlProcess : EtlProcess
    {
        private IHubContext hubContext;
        private IOperation operationWithProgressReporting;
        private const string RowsCountKey = "%rowsCount%";
        private static void PauseTaskForJustAMomentToLetUserInterfaceToLoad()
        {
            Thread.Sleep(4000);//It is done for task progress reporting to the ui. -Roman
        }

        protected override void Initialize()
        {
            PauseTaskForJustAMomentToLetUserInterfaceToLoad();

            hubContext = GlobalHost.ConnectionManager.GetHubContext<ProgressBarHub>();
            Progress = new Progress<double>(percent => hubContext.Clients.All.broadcastProgress(percent));
            Progress.Report(0); //Show the progress bar to the user
        }

        private static double CalculateProgress(long current, int total)
        {
            var progress = (current >= total) ? 100.00 : ((double)current / (double)total) * 100;
            return Math.Round(progress, 2);
        }

        private void ReportProgress(IOperation op, DynamicReader dictionary)
        {
            var rowsCount = 0;
            if (dictionary.Contains(RowsCountKey) && dictionary[RowsCountKey] != null)
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
                hubContext.Clients.All.broadcastError(e.Message);
                LogManager.GetLogger().Error(e.Message, e);
            });
        }

        protected void RegisterWithProgressReporting(IOperation op)
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