using AutoMapper;
using ElectronicRecyclers.One800Recycling.Application.AutoMapper;
using ElectronicRecyclers.One800Recycling.Application.ViewModels;
using ElectronicRecyclers.One800Recycling.Domain.Entities;


namespace ElectronicRecyclers.One800Recycling.Application.AutoMapper.Profiles
{
    public class RoleInputMapperProfile : DomainObjectMapperProfile<Role, RoleInput>
    {
        public RoleInputMapperProfile()
        {


            CreateMap<Role, RoleInput>()
                .ForMember(x => x.Name, o => o.MapFrom(m => m.Name))
                .ForMember(x => x.Description, o => o.MapFrom(m => m.Description));

            CreateMap<RoleInput, Role>()
                .ForMember(x => x.Name, o => o.MapFrom(m => m.Name))
                .ForMember(x => x.Description, o => o.MapFrom(m => m.Description));
        }
    }
}