using AutoMapper;
using ElectronicRecyclers.One800Recycling.Application.AutoMapper;
using ElectronicRecyclers.One800Recycling.Application.ViewModels;
using ElectronicRecyclers.One800Recycling.Domain.Common;
using ElectronicRecyclers.One800Recycling.Domain.Entities;
using ElectronicRecyclers.One800Recycling.Domain.ValueObjects;


namespace ElectronicRecyclers.One800Recycling.Application.AutoMapper.Profiles
{
    public class MaterialComponentInputMapperProfile 
        : DomainObjectMapperProfile<MaterialComponent, MaterialComponentInput>
    {
        public MaterialComponentInputMapperProfile()
        {
           

            CreateMap<MaterialComponent, MaterialComponentInput>()
                .ForMember(x => x.Name, o => o.MapFrom(m => m.Name))
                .ForMember(x => x.Description, o => o.MapFrom(m => m.Description))
                .ForMember(
                    x => x.SelectedVirginProductionProcessId,
                    o => o.MapFrom(m => m.DismantlingProcess.VirginProductionProcess.Id))
                .ForMember(
                    x => x.SelectedRecyclingProcessId,
                    o => o.MapFrom(m => m.DismantlingProcess.RecyclingProcess.Id))
                .ForMember(
                    x => x.SelectedLandfillingProcessId,
                    o => o.MapFrom(m => m.DismantlingProcess.LandfillingProcess.Id))
                .ForMember(
                    x => x.SelectedIncinerationProcessId,
                    o => o.MapFrom(m => m.DismantlingProcess.IncinerationProcess.Id));

            //TODO: Refactor to inject NHibernate session
            var session = NHSessionProvider.CurrentSession;

            CreateMap<MaterialComponentInput, MaterialComponent>()
                .ForMember(x => x.Name, o => o.MapFrom(m => m.Name))
                .ForMember(x => x.Description, o => o.MapFrom(m => m.Description))
                .ForMember(
                    x => x.DismantlingProcess,
                    o => o.MapFrom(m => new MaterialComponentDismantlingProcess(
                        session.Load<DismantlingProcess>(m.SelectedVirginProductionProcessId), 
                        session.Load<DismantlingProcess>(m.SelectedRecyclingProcessId), 
                        session.Load<DismantlingProcess>(m.SelectedLandfillingProcessId), 
                        session.Load<DismantlingProcess>(m.SelectedIncinerationProcessId))));
        }
    }
}