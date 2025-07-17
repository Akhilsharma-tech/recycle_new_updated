using System;
using System.Collections.Generic;
using ElectronicRecyclers.One800Recycling.Application.Common;
using ElectronicRecyclers.One800Recycling.Application.ETLProcess;
using ElectronicRecyclers.One800Recycling.Domain.ServiceObjects;




namespace ElectronicRecyclers.One800Recycling.Application.Import.Operations
{
    public class ReadFromService : AbstractOperation
    {
        private readonly Func<IEnumerable<IServiceEnvironmentalOrganization>> service;

        public ReadFromService(Func<IEnumerable<IServiceEnvironmentalOrganization>> service)
        {
            this.service = service;
        } 

        public override IEnumerable<DynamicReader> Execute(IEnumerable<DynamicReader> rows)
        {
            foreach (var obj in service())
            {
                yield return DynamicReader.FromObject(obj);
            }
        }
    }
}