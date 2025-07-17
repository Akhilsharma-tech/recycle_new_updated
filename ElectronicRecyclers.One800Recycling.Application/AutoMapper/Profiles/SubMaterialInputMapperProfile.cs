using AutoMapper;
using ElectronicRecyclers.One800Recycling.Application.ViewModels;
using ElectronicRecyclers.One800Recycling.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ElectronicRecyclers.One800Recycling.Application.AutoMapper.Profiles
{
    public class SubMaterialInputMapperProfile : DomainObjectMapperProfile<SubMaterials, SubMaterialInputViewModel>
    {
        public SubMaterialInputMapperProfile()
        {
          

            CreateMap<SubMaterials, SubMaterialInputViewModel>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id != null ? src.Id : 0))
           .ForMember(dest => dest.MaterialId, opt => opt.MapFrom(src => src.Material != null ? src.Material.Id : 0))
           .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));

        }
    }
}