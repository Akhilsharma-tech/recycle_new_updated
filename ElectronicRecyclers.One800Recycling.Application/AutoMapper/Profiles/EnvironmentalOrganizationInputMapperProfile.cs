using System.Linq;
using AutoMapper;
using ElectronicRecyclers.One800Recycling.Application.AutoMapper.Profiles.Resolvers;
using ElectronicRecyclers.One800Recycling.Application.ViewModels;
using ElectronicRecyclers.One800Recycling.Domain.Entities;


namespace ElectronicRecyclers.One800Recycling.Application.AutoMapper.Profiles
{
    public abstract class EnvironmentalOrganizationMapperProfile<TRecycler, TRecyclerInput>
        : DomainObjectMapperProfile<TRecycler, TRecyclerInput>
        where TRecycler : EnvironmentalOrganization
        where TRecyclerInput : EnvironmentalOrganizationInput
    {
        protected EnvironmentalOrganizationMapperProfile()
        {
           

            CreateMap<TRecycler, TRecyclerInput>()
                .ForMember(x => x.Id, o => o.MapFrom(m => m.Id))
                .ForMember(x => x.Name, o => o.MapFrom(m => m.Name))
                .ForMember(x => x.Description, o => o.MapFrom(m => m.Description))
                .ForMember(x => x.LogoImageUrl, o => o.MapFrom(m => m.LogoImageUrl))
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
                .ForMember(x => x.States, o => o.Ignore())
                .ForMember(x => x.Countries, o => o.Ignore())
                .ForMember(x => x.IsEnabled, o => o.MapFrom(m => m.IsEnabled))
                .ForMember(x => x.Notes, o => o.MapFrom(m => m.GetNotes()
                    .OrderByDescending(n => n.ModifiedOn)
                    .Select(note => new NoteSummary
                        {
                            Id = note.Id,
                            Text = note.Text,
                            AccessLevel = note.AccessLevel.ToString(),
                            ModifiedOn = note.ModifiedOn,
                            ModifiedBy = note.ModifiedBy
                        })))
                .ForMember(x => x.Materials, o => o.MapFrom(m => m.GetMaterials()
                    .OrderBy(material => material.Material.Name)
                    .Select(material => new EnvironmentalOrganizationInput.MaterialSummary
                        {
                            Id = material.Id,
                            MaterialId = material.Material.Id,
                            Name = material.Material.Name,
                            BusinessDeliveryOptions = material.GetMaterialDeliveries()
                                                            .Where(d => d.IsBusinessDelivery == true)
                                                            .Select(d => d.DeliveryType.ToString())
                                                            .ToList(),
                            ResidentialDeliveryOptions = material.GetMaterialDeliveries()
                                                            .Where(d => d.IsBusinessDelivery == false)
                                                            .Select(d => d.DeliveryType.ToString())
                                                            .ToList()
                        })))
                .ForMember(x => x.HoursOfOperations, o => o.MapFrom(m => m.GetHoursOfOperations()
                        .OrderBy(h => h.Rank)
                        .Select(hoursOfOperation => new EnvironmentalOrganizationInput.HoursOfOperationSummary
                        {
                            Id = hoursOfOperation.Id,
                            WeekDay = hoursOfOperation.DayOfWeek.ToString(),
                            OpenTime = hoursOfOperation.OpenTime,
                            CloseTime = hoursOfOperation.CloseTime,
                            AfterBreakOpenTime = hoursOfOperation.AfterBreakOpenTime,
                            AfterBreakCloseTime = hoursOfOperation.AfterBreakCloseTime,
                            IsClosed = hoursOfOperation.IsClosed,
                            Rank = hoursOfOperation.Rank
                        })));

            CreateMap<TRecyclerInput, TRecycler>()
                .ForMember(x => x.Id, o => o.Ignore())
                .ForMember(x => x.Name, o => o.MapFrom(m => m.Name))
                .ForMember(x => x.Description, o => o.MapFrom(m => m.Description))
                .ForMember(x => x.WebsiteUrl, o => o.MapFrom(m => m.WebsiteUrl))
                .ForMember(x => x.ImportBatchName, o => o.Ignore())
                .ForMember(x => x.Address, o => o.MapFrom(m => AddressResolver.Resolve(m)))                
                .ForMember(x => x.IsEnabled, o => o.MapFrom(m => m.IsEnabled))
                .ForMember(x => x.Telephone, o => o.MapFrom(m => PhoneResolver.Resolve(
                                                            m.SelectedPhoneCountryCode,
                                                            m.PhoneNumber
                                                            )))
                .ForMember(x => x.Fax, o => o.MapFrom(m => PhoneResolver.Resolve(
                                                            m.SelectedFaxCountryCode,
                                                            m.FaxNumber
                                                            )))
                .ForMember(x => x.LogoImageUrl, o => o.Ignore());
        }
    }
}