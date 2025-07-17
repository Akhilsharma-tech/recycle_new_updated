using AutoMapper;
using ElectronicRecyclers.One800Recycling.Application.ViewModels;
using ElectronicRecyclers.One800Recycling.Domain.ValueObjects;


namespace ElectronicRecyclers.One800Recycling.Application.AutoMapper.Profiles
{
    public class LookupCodeInputMapperProfile<TDomainObject, TViewModel> 
        : DomainObjectMapperProfile<TDomainObject, TViewModel>
        where TDomainObject : LookupCode
        where TViewModel : LookupCodeInput
    {
        protected LookupCodeInputMapperProfile()
        {
            

            CreateMap<TDomainObject, TViewModel>()
                .ForMember(x => x.Name, o => o.MapFrom(m => m.Name))
                .ForMember(x => x.Code, o => o.MapFrom(m => m.Code))
                .ForMember(x => x.Description, o => o.MapFrom(m => m.Description));

            CreateMap<TViewModel, TDomainObject>()
                .ForMember(x => x.Name, o => o.MapFrom(m => m.Name))
                .ForMember(x => x.Code, o => o.MapFrom(m => m.Code))
                .ForMember(x => x.Description, o => o.MapFrom(m => m.Description));
        }
    }
}