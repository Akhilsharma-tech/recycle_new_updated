using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using ElectronicRecyclers.One800Recycling.Application.Common;
using ElectronicRecyclers.One800Recycling.Domain.Entities;
using ElectronicRecyclers.One800Recycling.Web.ViewModels;


namespace ElectronicRecyclers.One800Recycling.Web.Infrastructure.AutoMapper.Profiles
{
    public class EnvironmentalOrganizationsViewModelMapperProfile : Profile
    {
        public  void CreateMap<T>() where T : EnvironmentalOrganization
        {
            CreateMap<T, EnvironmentalOrganizationsViewModel.OrganizationSummary>()
               .ForMember(x => x.Id, o => o.MapFrom(m => m.Id))
               .ForMember(x => x.Name, o => o.MapFrom(m => m.Name))
               .ForMember(x => x.Address, o => o.MapFrom(m => m.Address))
               .ForMember(x => x.Phone, o => o.MapFrom(m => m.Telephone))
               .ForMember(x => x.Fax, o => o.MapFrom(m => m.Fax))
               .ForMember(x => x.WebsiteUrl, o => o.MapFrom(m => m.WebsiteUrl))
               .ForMember(x => x.ImportBatchName, o => o.MapFrom(m => m.ImportBatchName))
               .ForMember(x => x.IsEnabled, o => o.MapFrom(m => m.IsEnabled))
               .ForMember(x => x.Materials, o => o.MapFrom(m => m.GetMaterials()
                                                 .Select(material => material.Material)))
               .ForMember(x => x.HoursOfOperations, o => o.MapFrom(m => m.GetHoursOfOperations()))
               .ForMember(x => x.Groups, o => o.MapFrom(m => m
                                    .GetVerificationGroups()
                                    .Select(g => g.Name)));

           CreateMap<PagedList<T>, PagedList<EnvironmentalOrganizationsViewModel.OrganizationSummary>>()
               .ForMember(x => x.HasNextPage, o => o.MapFrom(m => m.HasNextPage))
               .ForMember(x => x.HasPreviousPage, o => o.MapFrom(m => m.HasPreviousPage))
               .ForMember(x => x.LastItem, o => o.MapFrom(m => m.LastItem))
               .ForMember(x => x.PageIndex, o => o.MapFrom(m => m.PageIndex))
               .ForMember(x => x.PageSize, o => o.MapFrom(m => m.PageSize))
               .ForMember(x => x.TotalCount, o => o.MapFrom(m => m.TotalCount))
               .ForMember(x => x.TotalPages, o => o.MapFrom(m => m.TotalPages));
        }

        public EnvironmentalOrganizationsViewModelMapperProfile()
        {
            CreateMap<Recycler>();
            CreateMap<GovernmentAgencyOfAirQuality>();
            CreateMap<GovernmentAgencyOfWaterQuality>();
            CreateMap<MunicipalAgency>();
        }
    }
}