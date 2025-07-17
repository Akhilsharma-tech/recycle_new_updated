using System.Linq;
using AutoMapper;
using ElectronicRecyclers.One800Recycling.Application.ViewModels;
using ElectronicRecyclers.One800Recycling.Domain.Entities;


namespace ElectronicRecyclers.One800Recycling.Application.AutoMapper.Profiles
{
    public class MaterialInputMapperProfile : DomainObjectMapperProfile<Material, MaterialInput>
    {
        public MaterialInputMapperProfile()
        {
            

            CreateMap<Material, MaterialInput>()
                .ForMember(x => x.Id, o => o.MapFrom(m => m.Id))
                .ForMember(x => x.Name, o => o.MapFrom(m => m.Name))
                .ForMember(x => x.Description, o => o.MapFrom(m => m.Description))
                .ForMember(x => x.SearchKeywords, o => o.MapFrom(m => m.GetSearchKeywords()
                    .OrderBy(k => k)
                    .ToList()))
                .ForMember(x => x.IsEnabled, o => o.MapFrom(m => m.IsEnabled))
                .ForMember(x => x.Compositions, o => o.MapFrom(m => m.GetCompositions()
                    .OrderBy(c => c.MaterialComponent.Name)
                    .Select(c => new MaterialInput.MaterialCompositionViewModel
                    {
                        Id = c.Id,
                        MaterialComponentName = c.MaterialComponent.Name,
                        CompositionPercentage = c.CompositionPercentage
                    })))
                .ForMember(x => x.Processes, o => o.MapFrom(m => m.GetProcesses()
                    .OrderBy(p => p.ProductDismantlingProcess.ProductName)
                    .Select(p => new MaterialInput.ProcessViewModel
                    {
                        Id = p.Id,
                        ProductName = p.ProductDismantlingProcess.ProductName,
                        ProcessName = p.ProductDismantlingProcess.DismantlingProcess.Name,
                        CompositionPercentage = p.CompositionPercentage.ToString()
                    })))
                .ForMember(x => x.SubMaterials, o => o.MapFrom(m => m.GetSubMaterials()
                    .OrderBy(p => p.Name)
                 .Select(subMaterials => subMaterials.MapTo<SubMaterialInputViewModel>())))
                .ForMember(x => x.Categories, o => o.MapFrom(m => m.GetCategories()
                    .OrderBy(c => c.Name)
                    .Select(category => category.MapTo<MaterialCategoryViewModel>())));

            CreateMap<MaterialInput, Material>()
                .ForMember(x => x.Id, o => o.MapFrom(m => m.Id))
                .ForMember(x => x.Name, o => o.MapFrom(m => m.Name))
                .ForMember(x => x.Description, o => o.MapFrom(m => m.Description))
                .ForMember(x => x.IsEnabled, o => o.MapFrom(m => m.IsEnabled));

            CreateMap<MaterialCategory, MaterialCategoryViewModel>()
                .ForMember(x => x.Id, o => o.MapFrom(m => m.Id))
                .ForMember(x => x.Name, o => o.MapFrom(m => m.Name))
                .ForMember(x => x.Description, o => o.MapFrom(m => m.Description))
                .ForMember(x => x.IsEnabled, o => o.MapFrom(m => m.IsEnabled));


            CreateMap<SubMaterials, SubMaterialInputViewModel>()
                .ForMember(x => x.Id, o => o.MapFrom(m => m.Id))
                .ForMember(x => x.MaterialId, o => o.MapFrom(m => m.Material.Id))
                .ForMember(x => x.Name, o => o.MapFrom(m => m.Name));
                
        }
    }
}