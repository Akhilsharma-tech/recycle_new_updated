using ElectronicRecyclers.One800Recycling.Domain.Entities;
using ElectronicRecyclers.One800Recycling.Domain.Mappings.Types;
using NHibernate.Mapping.ByCode;

namespace ElectronicRecyclers.One800Recycling.Domain.Mappings
{
    public class MaterialMap : DomainObjectMap<Material>
    {
        public MaterialMap() 
        {
            Property(x => x.Name, m => 
                {
                    m.Length(100);
                    m.Unique(true);
                });

            Property(x => x.Description, m => m.Length(200));

            Property("searchKeywords", m =>
            {
                m.Length(4002);
                m.Column("SearchKeywords");
                m.Type<CommaDelimitedListType>();
                m.NotNullable(false);
            });

            Property(x => x.IsEnabled);

            Bag<MaterialCategory>("categories", m => 
            {
                m.Table("MaterialMaterialCategory");
                m.Cascade(Cascade.None);
                m.Lazy(CollectionLazy.Lazy);
                m.Key(k => k.Column("MaterialId"));
            },
            r => r.ManyToMany(c => c.Column("MaterialCategoryId")));

            Bag<MaterialComposition>("compositions", m =>
            {
                m.Table("MaterialComposition");
                m.Key(k => k.Column("MaterialId"));
                m.Cascade(Cascade.All | Cascade.DeleteOrphans);
                m.Lazy(CollectionLazy.Lazy);
                m.Inverse(true);
            },
            r => r.OneToMany());

            Bag<MaterialProductDismantlingProcess>("processes", m =>
            {
                m.Table("MaterialProductDismantlingProcess");
                m.Key(k => k.Column("MaterialId"));
                m.Cascade(Cascade.All | Cascade.DeleteOrphans);
                m.Lazy(CollectionLazy.Lazy);
                m.Inverse(true);
            },
            r => r.OneToMany());
        }
    }
}