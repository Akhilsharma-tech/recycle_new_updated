using AutoMapper;
using ElectronicRecyclers.One800Recycling.Domain.Common;


namespace ElectronicRecyclers.One800Recycling.Application.AutoMapper
{
    public class DomainObjectMapperProfile<TDomainObject, TViewModel> : Profile
        where TDomainObject : DomainObject
    {
        protected DomainObjectMapperProfile()
        {
            CreateMap<TDomainObject, TViewModel>()
                .ForSourceMember(x => x.CreatedBy, m => m.DoNotValidate())
                .ForSourceMember(x => x.CreatedOn, m => m.DoNotValidate())
                .ForSourceMember(x => x.ModifiedBy, m => m.DoNotValidate())
                .ForSourceMember(x => x.ModifiedOn, m => m.DoNotValidate())
                .ForSourceMember(x => x.Version, m => m.DoNotValidate());

            CreateMap<TViewModel, TDomainObject>()
                .ForMember(x => x.CreatedBy, m => m.Ignore())
                .ForMember(x => x.CreatedOn, m => m.Ignore())
                .ForMember(x => x.ModifiedBy, m => m.Ignore())
                .ForMember(x => x.ModifiedOn, m => m.Ignore())
                .ForMember(x => x.Version, m => m.Ignore());
        }
    }
}