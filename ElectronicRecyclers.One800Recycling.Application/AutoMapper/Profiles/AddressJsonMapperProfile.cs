using AutoMapper;
using ElectronicRecyclers.One800Recycling.Application.ViewModels;
using ElectronicRecyclers.One800Recycling.Domain.ValueObjects;


namespace ElectronicRecyclers.One800Recycling.Application.AutoMapper
{
    public class AddressJsonMapperProfile : Profile
    {
        public AddressJsonMapperProfile()
        {
             CreateMap<Address, AddressJson>()
                .ForMember(x => x.AddressLine1, o => o.MapFrom(m => m.AddressLine1))
                .ForMember(x => x.AddressLine2, o => o.MapFrom(m => m.AddressLine2))
                .ForMember(x => x.City, o => o.MapFrom(m => m.City))
                .ForMember(x => x.Region, o => o.MapFrom(m => m.Region))
                .ForMember(x => x.State, o => o.MapFrom(m => m.State))
                .ForMember(x => x.PostalCode, o => o.MapFrom(m => m.PostalCode))
                .ForMember(x => x.Country, o => o.MapFrom(m => m.Country))
                .ForMember(x => x.Latitude, o => o.MapFrom(m => m.Latitude))
                .ForMember(x => x.Longitude, o => o.MapFrom(m => m.Longitude));
        }
    }
}