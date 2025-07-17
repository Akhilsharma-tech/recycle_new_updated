using AutoMapper;
using ElectronicRecyclers.One800Recycling.Application.ViewModels;
using ElectronicRecyclers.One800Recycling.Domain.Entities;



namespace ElectronicRecyclers.One800Recycling.Application.AutoMapper.Profiles
{
    public class RecyclerInputMapperProfile : EnvironmentalOrganizationMapperProfile<Recycler, RecyclerInput>
    {
        public RecyclerInputMapperProfile()
        {


           CreateMap<Recycler, RecyclerInput>()
                .ForMember(x => x.IsDedicatedRecycler, o => o.MapFrom(m => m.IsDedicatedRecycler));

            CreateMap<RecyclerInput, Recycler>()
                .ForMember(x => x.IsDedicatedRecycler, o => o.MapFrom(m => m.IsDedicatedRecycler));
        }
    }
}