using AutoMapper;
using ElectronicRecyclers.One800Recycling.Application.AutoMapper;
using ElectronicRecyclers.One800Recycling.Application.ViewModels;
using ElectronicRecyclers.One800Recycling.Domain.Entities;


namespace ElectronicRecyclers.One800Recycling.Application.AutoMapper.Profiles
{
    public class RecyclingTipInputMapperProfile 
        : DomainObjectMapperProfile<RecyclingTip, RecyclingTipInput>
    {
        public RecyclingTipInputMapperProfile()
        {
            

            CreateMap<RecyclingTip, RecyclingTipInput>()
                .ForMember(x => x.Title, o => o.MapFrom(m => m.Title))
                .ForMember(x => x.Description, o => o.MapFrom(m => m.Description))
                .ForMember(x => x.ImageName, o => o.MapFrom(m => m.ImageName))
                .ForMember(x => x.ImageUrl, o => o.MapFrom(m => m.ImageName));

            CreateMap<RecyclingTipInput, RecyclingTip>()
                .ForMember(x => x.Title, o => o.MapFrom(m => m.Title))
                .ForMember(x => x.Description, o => o.MapFrom(m => m.Description))
                .ForMember(x => x.ImageName, o => o.MapFrom(m => m.ImageName))
                .ForMember(x => x.Number, o => o.Ignore());
        }
    }
}