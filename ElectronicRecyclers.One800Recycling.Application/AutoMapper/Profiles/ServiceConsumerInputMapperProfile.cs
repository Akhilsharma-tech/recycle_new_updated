using AutoMapper;
using ElectronicRecyclers.One800Recycling.Application.AutoMapper;
using ElectronicRecyclers.One800Recycling.Application.ViewModels;
using ElectronicRecyclers.One800Recycling.Domain.Entities;


namespace ElectronicRecyclers.One800Recycling.Web.Infrastructure.AutoMapper.Profiles
{
    public class ServiceConsumerInputMapperProfile 
        : DomainObjectMapperProfile<ServiceConsumer, ServiceConsumerInput>
    {
        public ServiceConsumerInputMapperProfile()
        {
        

            CreateMap<ServiceConsumer, ServiceConsumerInput>()
                .ForMember(x => x.Id, o => o.MapFrom(m => m.Id))
                .ForMember(x => x.WebsiteUrl, o => o.MapFrom(m => m.WebsiteUrl))
                .ForMember(x => x.Email, o => o.MapFrom(m => m.Email));

            CreateMap<ServiceConsumerInput, ServiceConsumer>()
                .ForMember(x => x.WebsiteUrl, o => o.MapFrom(m => m.WebsiteUrl))
                .ForMember(x => x.Email, o => o.MapFrom(m => m.Email));
        }
    }
}