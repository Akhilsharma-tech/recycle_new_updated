using AutoMapper;
using ElectronicRecyclers.One800Recycling.Application.ViewModels;
using ElectronicRecyclers.One800Recycling.Domain.Entities;

namespace ElectronicRecyclers.One800Recycling.Application.AutoMapper
{
    public class ChangePasswordInputMapperProfile 
        : DomainObjectMapperProfile<SystemUser, ChangePasswordInput>
    {
        public ChangePasswordInputMapperProfile()
        {
            CreateMap<SystemUser, ChangePasswordInput>()
                .ForMember(x => x.Id, o => o.MapFrom(m => m.Id))
                .ForMember(x => x.UserName, o => o.MapFrom(m => m.Name.ToTitleCase()));
        }
    }
}