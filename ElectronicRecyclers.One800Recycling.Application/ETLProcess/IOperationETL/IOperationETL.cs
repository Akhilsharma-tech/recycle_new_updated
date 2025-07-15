using ElectronicRecyclers.One800Recycling.Application.Common;
using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicRecyclers.One800Recycling.Application.ETLProcess.IOperationETL
{
    public interface IOperationETL : IDisposable
    {
        string Name { get; }
        Task<IEnumerable<DynamicReader>> ExecuteAsync(IEnumerable<DynamicReader> rows);
        event Action<DynamicReader> OnRowProcessed;
    }
}
