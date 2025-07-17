using System.Linq;
using AutoMapper;
using ElectronicRecyclers.One800Recycling.Application.AutoMapper;
using ElectronicRecyclers.One800Recycling.Domain.Entities;
using ElectronicRecyclers.One800Recycling.Application.ViewModels;
using ElectronicRecyclers.One800Recycling.Application.AutoMapper.Profiles.Resolvers;

namespace ElectronicRecyclers.One800Recycling.Application.AutoMapper.Profiles
{
    public class RecyclingEventInputMapperProfile
     : DomainObjectMapperProfile<RecyclingEvent, RecyclingEventInput>
    {
        public RecyclingEventInputMapperProfile()
        {
          

           CreateMap<RecyclingEvent, RecyclingEventInput>()
                .ForMember(x => x.Id, o => o.MapFrom(m => m.Id))
                .ForMember(x => x.Subject, o => o.MapFrom(m => m.Subject))
                .ForMember(x => x.Description, o => o.MapFrom(m => m.Description))
                .ForMember(x => x.StartOn, o => o.MapFrom(m => m.StartOn))
                .ForMember(x => x.EndOn, o => o.MapFrom(m => m.EndOn))
                .ForMember(x => x.WebsiteUrl, o => o.MapFrom(m => m.WebsiteUrl))
                .ForMember(x => x.AddressLine1, o => o.MapFrom(m => m.Address.AddressLine1))
                .ForMember(x => x.AddressLine2, o => o.MapFrom(m => m.Address.AddressLine2))
                .ForMember(x => x.City, o => o.MapFrom(m => m.Address.City))
                .ForMember(x => x.Region, o => o.MapFrom(m => m.Address.Region))
                .ForMember(x => x.SelectedStateCode, o => o.MapFrom(m => m.Address.State))
                .ForMember(x => x.PostalCode, o => o.MapFrom(m => m.Address.PostalCode))
                .ForMember(x => x.SelectedCountryCode, o => o.MapFrom(m => m.Address.Country))
                .ForMember(x => x.Latitude, o => o.MapFrom(m => m.Address.Latitude))
                .ForMember(x => x.Longitude, o => o.MapFrom(m => m.Address.Longitude))
                .ForMember(x => x.Materials, o => o.MapFrom(m => m.GetMaterials()
                    .Select(material => new RecyclingEventInput.MaterialSummary
                    {
                        Id = material.Id,
                        Name = material.Name
                    })))
                .ForMember(x => x.States, o => o.Ignore())
                .ForMember(x => x.Countries, o => o.Ignore());

            CreateMap<RecyclingEventInput, RecyclingEvent>()
                .ForMember(x => x.Id, o => o.MapFrom(m => m.Id))
                .ForMember(x => x.Subject, o => o.MapFrom(m => m.Subject))
                .ForMember(x => x.Description, o => o.MapFrom(m => m.Description))
                .ForMember(x => x.StartOn, o => o.MapFrom(m => m.StartOn))
                .ForMember(x => x.EndOn, o => o.MapFrom(m => m.EndOn))
                .ForMember(x => x.WebsiteUrl, o => o.MapFrom(m => m.WebsiteUrl))
                .ForMember(x => x.Address, o => o.MapFrom(m => AddressResolver.Resolve(m)));
        }
    }
}