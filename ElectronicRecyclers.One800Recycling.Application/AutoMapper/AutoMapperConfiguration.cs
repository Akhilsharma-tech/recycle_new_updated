using AutoMapper;
using ElectronicRecyclers.One800Recycling.Application.AutoMapper.Profiles;
using ElectronicRecyclers.One800Recycling.Web.Infrastructure.AutoMapper.Profiles;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicRecyclers.One800Recycling.Application.AutoMapper
{
    public static class AutoMapperConfigExtensions
    {
        public static void AddAutoMapperConfiguration(this IServiceCollection services)
        {
            services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<MaterialInputMapperProfile>();
                cfg.AddProfile<MaterialCategoryInputMapperProfile>();
                cfg.AddProfile<RecyclingEventInputMapperProfile>();
                cfg.AddProfile<SystemUserInputMapperProfile>();
                cfg.AddProfile<RoleInputMapperProfile>();
                cfg.AddProfile<RecyclerInputMapperProfile>();
                cfg.AddProfile<MunicipalAgencyInputMapperProfile>();
                cfg.AddProfile<NoteInputMapperProfile>();
                cfg.AddProfile<CountryInputMapperProfile>();
                cfg.AddProfile<StateInputMapperProfile>();
                cfg.AddProfile<PhoneCountryCodeInputMapperProfile>();
                cfg.AddProfile<ChangePasswordInputMapperProfile>();
                cfg.AddProfile<HoursOfOperationInputMapperProfile>();
                cfg.AddProfile<GovernmentAgencyOfAirQualityInputMapperProfile>();
                cfg.AddProfile<GovernmentAgencyOfWaterQualityInputMapperProfile>();
                cfg.AddProfile<EnvironmentalOrganizationImportInputMapperProfile>();
                cfg.AddProfile<EnvironmentalOrganizationsViewModelMapperProfile>();
                cfg.AddProfile<PostalCodeInputMapperProfile>();
                cfg.AddProfile<SmartyStreetsAddressMapperProfile>();
                cfg.AddProfile<AddressJsonMapperProfile>();
                cfg.AddProfile<ServiceConsumerInputMapperProfile>();
                cfg.AddProfile<RecyclingTipInputMapperProfile>();
                cfg.AddProfile<MaterialComponentInputMapperProfile>();
                cfg.AddProfile<DismantlingProcessInputMapperProfile>();
                cfg.AddProfile<ProductDismantlingProcessInputMapperProfile>();
                cfg.AddProfile<EnvironmentalOrganizationComparisonResultsViewModelMapperProfile>();
                cfg.AddProfile<SubMaterialInputMapperProfile>();
            });
        }
    }
}

