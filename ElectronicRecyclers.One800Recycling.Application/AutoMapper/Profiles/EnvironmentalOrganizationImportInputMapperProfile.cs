using AutoMapper;
using ElectronicRecyclers.One800Recycling.Application.AutoMapper.Profiles.Resolvers;
using ElectronicRecyclers.One800Recycling.Application.ViewModels;
using ElectronicRecyclers.One800Recycling.Domain.Entities;


namespace ElectronicRecyclers.One800Recycling.Application.AutoMapper.Profiles
{
    public class EnvironmentalOrganizationImportInputMapperProfile
        : DomainObjectMapperProfile<EnvironmentalOrganizationImport, EnvironmentalOrganizationImportInput>
    {
        public EnvironmentalOrganizationImportInputMapperProfile()
        {
            

            CreateMap<EnvironmentalOrganizationImport, EnvironmentalOrganizationImportInput>()
                .ForMember(x => x.Id, o => o.MapFrom(m => m.Id))
                .ForMember(x => x.Name, o => o.MapFrom(m => m.Name))
                .ForMember(x => x.Description, o => o.MapFrom(m => m.Description))
                .ForMember(x => x.WebsiteUrl, o => o.MapFrom(m => m.WebsiteUrl))
                .ForMember(x => x.SelectedPhoneCountryCode, o => o.MapFrom(m => m.Telephone.CountryCodeSource))
                .ForMember(x => x.PhoneNumber, o => o.MapFrom(m => m.Telephone.Number))
                .ForMember(x => x.SelectedFaxCountryCode, o => o.MapFrom(m => m.Fax.CountryCodeSource))
                .ForMember(x => x.FaxNumber, o => o.MapFrom(m => m.Fax.Number))
                .ForMember(x => x.AddressLine1, o => o.MapFrom(m => m.Address.AddressLine1))
                .ForMember(x => x.AddressLine2, o => o.MapFrom(m => m.Address.AddressLine2))
                .ForMember(x => x.City, o => o.MapFrom(m => m.Address.City))
                .ForMember(x => x.Region, o => o.MapFrom(m => m.Address.Region))
                .ForMember(x => x.SelectedStateCode, o => o.MapFrom(m => m.Address.State))
                .ForMember(x => x.PostalCode, o => o.MapFrom(m => m.Address.PostalCode))
                .ForMember(x => x.SelectedCountryCode, o => o.MapFrom(m => m.Address.Country))
                .ForMember(x => x.Latitude, o => o.MapFrom(m => m.Address.Latitude))
                .ForMember(x => x.Longitude, o => o.MapFrom(m => m.Address.Longitude))
                .ForMember(x => x.HoursOfOperation, o => o.MapFrom(m => m.HoursOfOperation))
                .ForMember(x => x.PrivateNote, o => o.MapFrom(m => m.PrivateNote))
                .ForMember(x => x.PublicNote, o => o.MapFrom(m => m.PublicNote))
                .ForMember(x => x.OrganizationType, o => o.MapFrom(m => m.Type))
                .ForMember(x => x.States, o => o.Ignore())
                .ForMember(x => x.Countries, o => o.Ignore());

            CreateMap<EnvironmentalOrganizationImportInput, EnvironmentalOrganizationImport>()
                .ForMember(x => x.Id, o => o.Ignore())
                .ForMember(x => x.Type, o => o.Ignore())
                .ForMember(x => x.Name, o => o.MapFrom(m => m.Name))
                .ForMember(x => x.Description, o => o.MapFrom(m => m.Description))
                .ForMember(x => x.WebsiteUrl, o => o.MapFrom(m => m.WebsiteUrl))
                .ForMember(x => x.HoursOfOperation, o => o.MapFrom(m => m.HoursOfOperation))
                .ForMember(x => x.PrivateNote, o => o.MapFrom(m => m.PrivateNote))
                .ForMember(x => x.PublicNote, o => o.MapFrom(m => m.PublicNote))
                .ForMember(x => x.Address, o => o.MapFrom(m => AddressResolver.Resolve(m)))
                .ForMember(x => x.Telephone, o => o.MapFrom(m => PhoneResolver.Resolve(
                                                            m.SelectedPhoneCountryCode,
                                                            m.PhoneNumber)))
                .ForMember(x => x.Fax, o => o.MapFrom(m => PhoneResolver.Resolve(
                                                            m.SelectedFaxCountryCode,
                                                            m.FaxNumber)));
        }
    }
}