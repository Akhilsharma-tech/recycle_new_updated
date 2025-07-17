using System.Linq;
using AutoMapper;
using ElectronicRecyclers.One800Recycling.Application.AutoMapper;
using ElectronicRecyclers.One800Recycling.Application.Common;
using ElectronicRecyclers.One800Recycling.Application.ViewModels;
using ElectronicRecyclers.One800Recycling.Domain.Entities;
using ElectronicRecyclers.One800Recycling.Domain.ValueObjects;



namespace ElectronicRecyclers.One800Recycling.Application.AutoMapper.Profiles
{
    public class SystemUserInputMapperProfile : DomainObjectMapperProfile<SystemUser, SystemUserInput>
    {
        public SystemUserInputMapperProfile()
        {
            

            CreateMap<SystemUser, SystemUserInput>()
                .ForMember(x => x.FirstName, o => o.MapFrom(m => m.Name.FirstName.ToTitleCase()))
                .ForMember(x => x.LastName, o => o.MapFrom(m => m.Name.LastName.ToTitleCase()))
                .ForMember(x => x.Email, o => o.MapFrom(m => m.Email))
                .ForMember(x => x.Password, o => o.Ignore())
                .ForMember(x => x.ConfirmPassword, o => o.Ignore())
                .ForMember(x => x.IsEnabled, o => o.MapFrom(m => m.IsEnabled))
                .ForMember(x => x.Roles, o => o.MapFrom(m => m.GetRoles()
                    .Select(role => new SystemUserInput.RoleSummary 
                            {
                                Id = role.Id,
                                Name = role.Name,
                                Description = role.Description
                            })))

                .ForMember(x => x.SelectedRoleIds, o => o.Ignore());

            CreateMap<SystemUserInput, SystemUser>()
                .ForMember(x => x.Name, o => o.MapFrom(m => new Name(m.FirstName, m.LastName)))
                .ForMember(x => x.Email, o => o.MapFrom(m => m.Email))
                .ForMember(x => x.IsEnabled, o => o.MapFrom(m => m.IsEnabled))
                .ForMember(x => x.PasswordSalt, o => o.Ignore())
                .ForMember(x => x.HashedPassword, o => o.Ignore())
                .ForMember(x => x.LastLoginOn, o => o.Ignore());
        }
    }
}