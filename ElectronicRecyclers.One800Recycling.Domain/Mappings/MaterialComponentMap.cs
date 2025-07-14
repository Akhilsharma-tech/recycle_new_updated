using ElectronicRecyclers.One800Recycling.Domain.Entities;
using NHibernate.Mapping.ByCode;

namespace ElectronicRecyclers.One800Recycling.Domain.Mappings
{
    public class MaterialComponentMap : DomainObjectMap<MaterialComponent>
    {
        public MaterialComponentMap()
        {
            Property(x => x.Name, m => m.Length(100));

            Property(x => x.Description, m => m.Length(200));

            Component(x => x.DismantlingProcess, m =>
            {
                m.ManyToOne<DismantlingProcess>(
                    "VirginProductionProcess",
                    map =>
                    {
                        map.ForeignKey("FK_DismantlingProcess_MaterialComponent_VirginProductionDismantlingProcess");
                        map.Column("VirginProductionDismantlingProcessId");
                        map.Cascade(Cascade.None);
                    });

                m.ManyToOne<DismantlingProcess>(
                    "RecyclingProcess",
                    map =>
                    {
                        map.ForeignKey("FK_DismantlingProcess_MaterialComponent_RecyclingDismantlingProcess");
                        map.Column("RecyclingDismantlingProcessId");
                        map.Cascade(Cascade.None);
                    });

                m.ManyToOne<DismantlingProcess>(
                    "LandfillingProcess",
                    map =>
                    {
                        map.ForeignKey("FK_DismantlingProcess_MaterialComponent_LandfillingDismantlingProcess");
                        map.Column("LandfillingDismantlingProcessId");
                        map.Cascade(Cascade.None);
                    });

                m.ManyToOne<DismantlingProcess>(
                    "IncinerationProcess",
                    map =>
                    {
                        map.ForeignKey("FK_DismantlingProcess_MaterialComponent_IncinerationDismantlingProcess");
                        map.Column("IncinerationDismantlingProcessId");
                        map.Cascade(Cascade.None);
                    });
            });
        }
    }
}