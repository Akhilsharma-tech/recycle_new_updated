using AutoMapper;
using ElectronicRecyclers.One800Recycling.Application.ViewModels;
using ElectronicRecyclers.One800Recycling.Domain.ValueObjects;


namespace ElectronicRecyclers.One800Recycling.Application.AutoMapper.Profiles
{
    public class PostalCodeInputMapperProfile 
        : DomainObjectMapperProfile<PostalCode, PostalCodeInput>
    {
        public PostalCodeInputMapperProfile()
        {


           CreateMap<PostalCode, PostalCodeInput>()
                .ForMember(x => x.Id, o => o.MapFrom(m => m.Id))
                .ForMember(x => x.PostalCode, o => o.MapFrom(m => m.Code))
                .ForMember(x => x.City, o => o.MapFrom(m => m.City))
                .ForMember(x => x.Region, o => o.MapFrom(m => m.Region))
                .ForMember(x => x.SelectedStateCode, o => o.MapFrom(m => m.State))
                .ForMember(x => x.SelectedCountryCode, o => o.MapFrom(m => m.Country))
                .ForMember(x => x.Latitude, o => o.MapFrom(m => m.Latitude))
                .ForMember(x => x.Longitude, o => o.MapFrom(m => m.Longitude));

            CreateMap<PostalCodeInput, PostalCode>()
                .ForMember(x => x.Id, o => o.MapFrom(m => m.Id))
                .ForMember(x => x.Code, o => o.MapFrom(m => m.PostalCode))
                .ForMember(x => x.City, o => o.MapFrom(m => m.City))
                .ForMember(x => x.Region, o => o.MapFrom(m => m.Region))
                .ForMember(x => x.State, o => o.MapFrom(m => m.SelectedStateCode))
                .ForMember(x => x.Country, o => o.MapFrom(m => m.SelectedCountryCode))
                .ForMember(x => x.Latitude, o => o.MapFrom(m => m.Latitude))
                .ForMember(x => x.Longitude, o => o.MapFrom(m => m.Longitude));
        }
    }
}