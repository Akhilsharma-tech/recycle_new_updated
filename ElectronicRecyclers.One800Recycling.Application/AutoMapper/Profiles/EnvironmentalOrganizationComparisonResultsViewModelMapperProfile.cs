using AutoMapper;
using ElectronicRecyclers.One800Recycling.Application.Common;
using ElectronicRecyclers.One800Recycling.Application.ViewModels;
using ElectronicRecyclers.One800Recycling.Domain.Entities;
using ElectronicRecyclers.One800Recycling.Web.ViewModels;

namespace ElectronicRecyclers.One800Recycling.Application.AutoMapper.Profiles
{
    public class EnvironmentalOrganizationComparisonResultsViewModelMapperProfile : Profile
    {
        public  void CreateMap<T>() where T : EnvironmentalOrganization
        {
           CreateMap<T, EnvironmentalOrganizationComparisonResultsViewModel.OrganizationSummary>()
                .ForMember(x => x.Id, o => o.MapFrom(m => m.Id))
                .ForMember(x => x.Name, o => o.MapFrom(m => m.Name))
                .ForMember(x => x.Address, o => o.MapFrom(m => m.Address))
                .ForMember(x => x.ImportBatchName, o => o.MapFrom(m => m.ImportBatchName))
                .ForMember(x => x.Telephone, o => o.MapFrom(m => m.Telephone))
                .ForMember(x => x.Fax, o => o.MapFrom(m => m.Fax));

          CreateMap<PagedList<T>, PagedList<EnvironmentalOrganizationComparisonResultsViewModel.OrganizationSummary>>()
               .ForMember(x => x.HasNextPage, o => o.MapFrom(m => m.HasNextPage))
               .ForMember(x => x.HasPreviousPage, o => o.MapFrom(m => m.HasPreviousPage))
               .ForMember(x => x.LastItem, o => o.MapFrom(m => m.LastItem))
               .ForMember(x => x.PageIndex, o => o.MapFrom(m => m.PageIndex))
               .ForMember(x => x.PageSize, o => o.MapFrom(m => m.PageSize))
               .ForMember(x => x.TotalCount, o => o.MapFrom(m => m.TotalCount))
               .ForMember(x => x.TotalPages, o => o.MapFrom(m => m.TotalPages));
        }

        private  void CreateOrganizationImportMap()
        {
            CreateMap<EnvironmentalOrganizationImport, EnvironmentalOrganizationComparisonResultsViewModel.OrganizationSummary>()
                .ForMember(x => x.Id, o => o.MapFrom(m => m.Id))
                .ForMember(x => x.Name, o => o.MapFrom(m => m.Name))
                .ForMember(x => x.Address, o => o.MapFrom(m => m.Address))
                .ForMember(x => x.ImportBatchName, o => o.MapFrom(m => m.ImportBatchName))
                .ForMember(x => x.Telephone, o => o.MapFrom(m => m.Telephone))
                .ForMember(x => x.Fax, o => o.MapFrom(m => m.Fax));

            CreateMap<PagedList<EnvironmentalOrganizationImport>, PagedList<EnvironmentalOrganizationComparisonResultsViewModel.OrganizationSummary>>()
               .ForMember(x => x.HasNextPage, o => o.MapFrom(m => m.HasNextPage))
               .ForMember(x => x.HasPreviousPage, o => o.MapFrom(m => m.HasPreviousPage))
               .ForMember(x => x.LastItem, o => o.MapFrom(m => m.LastItem))
               .ForMember(x => x.PageIndex, o => o.MapFrom(m => m.PageIndex))
               .ForMember(x => x.PageSize, o => o.MapFrom(m => m.PageSize))
               .ForMember(x => x.TotalCount, o => o.MapFrom(m => m.TotalCount))
               .ForMember(x => x.TotalPages, o => o.MapFrom(m => m.TotalPages));
        }

        public EnvironmentalOrganizationComparisonResultsViewModelMapperProfile()
        {
            //TODO: Refactor create map methods to use only one mapping method
            CreateMap<Recycler>();
            CreateMap<GovernmentAgencyOfAirQuality>();
            CreateMap<GovernmentAgencyOfWaterQuality>();
            CreateMap<MunicipalAgency>();
            CreateOrganizationImportMap();
        }
    }
}