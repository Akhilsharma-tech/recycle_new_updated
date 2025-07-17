using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using ElectronicRecyclers.One800Recycling.Application.ViewModels;
using ElectronicRecyclers.One800Recycling.Domain.Entities;


namespace ElectronicRecyclers.One800Recycling.Application.AutoMapper.Profiles
{
    public class HoursOfOperationInputMapperProfile 
        : DomainObjectMapperProfile<HoursOfOperation, HoursOfOperationInput>
    {
        public HoursOfOperationInputMapperProfile()
        {
            

            CreateMap<HoursOfOperation, HoursOfOperationInput>()
                .ForMember(x => x.Id, o => o.MapFrom(m => m.Id))
                .ForMember(x => x.SelectedOpenTime, o => o.MapFrom(m => m.OpenTime.ToString()))
                .ForMember(x => x.SelectedCloseTime, o => o.MapFrom(m => m.CloseTime.ToString()));
        }
    }
}