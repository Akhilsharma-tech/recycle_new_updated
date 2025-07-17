using AutoMapper;
using ElectronicRecyclers.One800Recycling.Application.AutoMapper;
using ElectronicRecyclers.One800Recycling.Application.ViewModels;
using ElectronicRecyclers.One800Recycling.Domain.Entities;


namespace ElectronicRecyclers.One800Recycling.Application.AutoMapper
{
    public class MaterialCategoryInputMapperProfile 
        : DomainObjectMapperProfile<MaterialCategory, MaterialCategoryInput>
    {
        public MaterialCategoryInputMapperProfile()
        {
        

          CreateMap<MaterialCategory, MaterialCategoryInput>()
                .ForMember(x => x.Id, o => o.MapFrom(m => m.Id))
                .ForMember(x => x.Name, o => o.MapFrom(m => m.Name))
                .ForMember(x => x.Description, o => o.MapFrom(m => m.Description))
                .ForMember(x => x.IsEnabled, o => o.MapFrom(m => m.IsEnabled));

          CreateMap<MaterialCategoryInput, MaterialCategory>()
                .ForMember(x => x.Id, o => o.MapFrom(m => m.Id))
                .ForMember(x => x.Name, o => o.MapFrom(m => m.Name))
                .ForMember(x => x.Description, o => o.MapFrom(m => m.Description))
                .ForMember(x => x.IsEnabled, o => o.MapFrom(m => m.IsEnabled));
        }
    }
}