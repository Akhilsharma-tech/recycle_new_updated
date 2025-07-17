using ElectronicRecyclers.One800Recycling.Application.Common;
using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicRecyclers.One800Recycling.Application.ETLProcess
{
    public interface IOperationETL : IDisposable
    {

        string Name { get; }
        bool UseTransaction { get; set; }
        OperationStatistics Statistics { get; }

        event Action<IOperationETL, DynamicReader>? OnRowProcessed;
        event Action<IOperationETL>? OnFinishedProcessing;

        void PrepareForExecution(IPipelineExecuter pipelineExecuter);
        void RaiseRowProcessed(DynamicReader row);
        void RaiseFinishedProcessing();
        IEnumerable<Exception> GetAllErrors();
        IEnumerable<DynamicReader> Execute(IEnumerable<DynamicReader> rows);
        
    }
}
