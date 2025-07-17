using AutoMapper;
using ElectronicRecyclers.One800Recycling.Application.ViewModels;
using ElectronicRecyclers.One800Recycling.Domain.Common;
using ElectronicRecyclers.One800Recycling.Domain.Entities;


namespace ElectronicRecyclers.One800Recycling.Application.AutoMapper.Profiles
{
    public class ProductDismantlingProcessInputMapperProfile 
        : DomainObjectMapperProfile<ProductDismantlingProcess, ProductDismantlingProcessInput>
    {
        public ProductDismantlingProcessInputMapperProfile()
        {
            

            CreateMap<ProductDismantlingProcess, ProductDismantlingProcessInput>()
                .ForMember(x => x.ProductName, o => o.MapFrom(m => m.ProductName))
                .ForMember(x => x.SelectedDismantlingProcessId, o => o.MapFrom(m => m.DismantlingProcess.Id));

            //TODO: Refactor to inject the session
            var session = NHSessionProvider.CurrentSession;

            CreateMap<ProductDismantlingProcessInput, ProductDismantlingProcess>()
                .ForMember(x => x.ProductName, o => o.MapFrom(m => m.ProductName))
                .ForMember(
                    x => x.DismantlingProcess,
                    o => o.MapFrom(m => session.Load<DismantlingProcess>(m.SelectedDismantlingProcessId)));
        }
    }
}