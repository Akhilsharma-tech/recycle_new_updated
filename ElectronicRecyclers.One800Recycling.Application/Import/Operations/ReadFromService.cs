using System;
using System.Collections.Generic;
using ElectronicRecyclers.One800Recycling.Domain.ServiceObjects;
using ElectronicRecyclers.One800Recycling.Web.Models.ServiceObjects;



namespace ElectronicRecyclers.One800Recycling.Application.Import.Operations
{
    public class ReadFromService 
    {
        private readonly Func<IEnumerable<IServiceEnvironmentalOrganization>> service;

        public ReadFromService(Func<IEnumerable<IServiceEnvironmentalOrganization>> service)
        {
            this.service = service;
        } 

        public override IEnumerable<Dictionary<string,object>> Execute(IEnumerable<Dictionary<string,object>> rows)
        {
            foreach (var obj in service())
            {
                yield return Row.FromObject(obj);
            }
        }
    }
}