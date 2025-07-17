using AutoMapper;
using ElectronicRecyclers.One800Recycling.Application.AutoMapper;
using ElectronicRecyclers.One800Recycling.Application.ViewModels;
using ElectronicRecyclers.One800Recycling.Domain.Entities;
using ElectronicRecyclers.One800Recycling.Domain.ValueObjects;


namespace ElectronicRecyclers.One800Recycling.Application.AutoMapper.Profiles
{
    public class DismantlingProcessInputMapperProfile 
        : DomainObjectMapperProfile<DismantlingProcess, DismantlingProcessInput>
    {
        public DismantlingProcessInputMapperProfile()
        {
            CreateMap<DismantlingProcess, DismantlingProcessInput>()
                .ForMember(x => x.Id, o => o.MapFrom(m => m.Id))
                .ForMember(x => x.Name, o => o.MapFrom(m => m.Name))
                .ForMember(x => x.SelectedType, o => o.MapFrom(m => m.Type))
                .ForMember(x => x.Types, o => o.Ignore())
                .ForMember(
                    x => x.LossPercentageDuringRecyclingProcess,
                    o => o.MapFrom(m => m.LossPercentageDuringRecycling))
                .ForMember(
                    x => x.ClimateChangeImpact, 
                    o => o.MapFrom(m => m.EnvironmentalImpact.ClimateChangeImpact))
                .ForMember(
                    x => x.ResourceDepletionImpact,
                    o => o.MapFrom(m => m.EnvironmentalImpact.ResourceDepletionImpact))
                .ForMember(
                    x => x.WaterWithdrawalImpact,
                    o => o.MapFrom(m => m.EnvironmentalImpact.WaterWithdrawalImpact));

            CreateMap<DismantlingProcessInput, DismantlingProcess>()
                .ForMember(x => x.Id, o => o.MapFrom(m => m.Id))
                .ForMember(x => x.Name, o => o.MapFrom(m => m.Name))
                .ForMember(x => x.Type, o => o.MapFrom(m => m.SelectedType))
                .ForMember(
                    x => x.EnvironmentalImpact,
                    o => o.MapFrom(m => new EnvironmentalImpact(
                        (m.ClimateChangeImpact.HasValue) ? m.ClimateChangeImpact.Value : 0,
                        (m.ResourceDepletionImpact.HasValue) ? m.ResourceDepletionImpact.Value : 0,
                        (m.WaterWithdrawalImpact.HasValue) ? m.WaterWithdrawalImpact.Value : 0)))
                .ForMember(
                    x => x.LossPercentageDuringRecycling,
                    o => o.MapFrom(m => (m.LossPercentageDuringRecyclingProcess.HasValue)
                                                ? m.LossPercentageDuringRecyclingProcess.Value
                                                : 0));

        }
    }
}