using System.Collections.Generic;
using AutoMapper;
using ElectronicRecyclers.One800Recycling.Application.AutoMapper.Profiles.Resolvers;
using ElectronicRecyclers.One800Recycling.Application.ViewModels;
using ElectronicRecyclers.One800Recycling.Domain.ValueObjects;
using ElectronicRecyclers.One800Recycling.Web.Infrastructure.AutoMapper.Profiles;


namespace ElectronicRecyclers.One800Recycling.Application.AutoMapper.Profiles
{
    public class SmartyStreetsAddressMapperProfile : Profile
    {
        public SmartyStreetsAddressMapperProfile()
        {
            CreateMap<KeyValuePair<int, Address>, SmartyStreetsRequestJson>()
                .ForMember(x => x.Id, o => o.MapFrom(m => m.Key))
                .ForMember(x => x.AddressLine1, o => o.MapFrom(m => m.Value.AddressLine1))
                .ForMember(x => x.AddressLine2, o => o.MapFrom(m => m.Value.AddressLine2))
                .ForMember(x => x.City, o => o.MapFrom(m => m.Value.City))
                .ForMember(x => x.State, o => o.MapFrom(m => m.Value.State))
                .ForMember(x => x.PostalCode, o => o.MapFrom(m => m.Value.PostalCode));

            CreateMap<SmartyStreetsResponseJson, KeyValuePair<int, Address>>()
                .ConstructUsing(x => new KeyValuePair<int, Address>(int.Parse(x.Id), AddressResolver.Resolve(x)));
        }
    }
}