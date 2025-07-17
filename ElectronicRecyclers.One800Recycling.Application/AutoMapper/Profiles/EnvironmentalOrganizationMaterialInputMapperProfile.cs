using System;
using System.Collections.ObjectModel;
using System.Linq;
using AutoMapper;
using ElectronicRecyclers.One800Recycling.Application.ViewModels;
using ElectronicRecyclers.One800Recycling.Domain.Entities;


namespace ElectronicRecyclers.One800Recycling.Application.AutoMapper.Profiles
{
    public class EnvironmentalOrganizationMaterialInputMapperProfile 
        : DomainObjectMapperProfile<EnvironmentalOrganizationMaterial, 
        EnvironmentalOrganizationInput.MaterialInput>
    {
        protected EnvironmentalOrganizationMaterialInputMapperProfile()
        {
            

           CreateMap<EnvironmentalOrganizationMaterial, EnvironmentalOrganizationInput.MaterialInput>()
                .ForMember(x => x.OrganizationId, o => o.MapFrom(m => m.Organization.Id))
                .ForMember(x => x.DeliveryOptions, o => o.MapFrom(m => Enum.GetNames(typeof(MaterialDeliveryType))))
                .ForMember(x => x.Materials, o => o.MapFrom(m => new Collection<Material>()))
                .ForMember(x => x.SelectedBusinessDeliveryOptions, o => o.MapFrom(m => m.GetMaterialDeliveries()
                            .Where(d => d.IsBusinessDelivery == true)
                            .Select(d => d.DeliveryType.ToString()))
                            )
                .ForMember(x => x.SelectedResidentialDeliveryOptions, o => o.MapFrom(m => m.GetMaterialDeliveries()
                            .Where(d => d.IsBusinessDelivery == false)
                            .Select(d => d.DeliveryType.ToString()))
                            );
        }
    }
}