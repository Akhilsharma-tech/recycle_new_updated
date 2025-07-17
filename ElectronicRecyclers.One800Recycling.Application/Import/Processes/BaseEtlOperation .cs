using System;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Threading;
using ElectronicRecyclers.One800Recycling.Application.Common;

using ElectronicRecyclers.One800Recycling.Application.Import.Operations;
using ElectronicRecyclers.One800Recycling.Application.Logging;
using Microsoft.AspNetCore.SignalR;
using Microsoft.CodeAnalysis;




namespace ElectronicRecyclers.One800Recycling.Application.Import.Processes
{
    //public abstract class BaseEtlOperation : IOperationETL
    //{

    //    public abstract string Name { get; }

    //    public event Action<DynamicReader> OnRowProcessed;

    //    protected void ReportRowProcessed(DynamicReader row)
    //    {
    //        OnRowProcessed?.Invoke(row);
    //    }

    //    public abstract Task<IEnumerable<DynamicReader>> ExecuteAsync(IEnumerable<DynamicReader> rows);

    //    public virtual void Dispose() { }

    //}
}