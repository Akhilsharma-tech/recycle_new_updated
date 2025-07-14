using NHibernate.Mapping.ByCode;

namespace ElectronicRecyclers.One800Recycling.Domain.Mappings
{
    /// <summary>
    /// Configure common database schema conventions
    /// </summary>
    public class MappingConventionConfiguration
    {
        private static void SetColumnNotNullConvention(
            IModelInspector inspector, 
            PropertyPath member, 
            IPropertyMapper customizer)
        {
            customizer.NotNullable(true);
        }

        private static void SetColumnNotNullConvention(
            IModelInspector inspector, 
            PropertyPath member, 
            IManyToOneMapper customizer)
        {
            customizer.NotNullable(true);
        }

        private static void SetForeignKeyNameConvention(
            IModelInspector inspector, 
            PropertyPath member, 
            IManyToOneMapper customizer)
        {
            var memberType = member.LocalMember.GetPropertyOrFieldType();
            customizer.Column(string.Format("{0}Id", memberType.Name));

            var foreignKeyEntityName = member.GetContainerEntity(inspector).Name;
            customizer.ForeignKey(string.Format("FK_{0}_{1}", memberType.Name, foreignKeyEntityName));
        }

        private static void SetForeignKeyNameConvention(
            IModelInspector inspector, 
            PropertyPath member, 
            IBagPropertiesMapper customizer)
        {
            var entityName = member.GetContainerEntity(inspector).Name;

            var foreignKeyEntityName = member
                .GetRootMember()
                .GetPropertyOrFieldType()
                .DetermineCollectionElementOrDictionaryValueType()
                .Name;

            customizer.Key(x =>
            {
                x.Column(string.Format("{0}Id", entityName));
                x.ForeignKey(string.Format("FK_{0}_{1}", entityName, foreignKeyEntityName));
            });
        }

        public static void Configure(ModelMapper mapper)
        {
            mapper.BeforeMapProperty += SetColumnNotNullConvention;
            mapper.BeforeMapManyToOne += SetColumnNotNullConvention;
            mapper.BeforeMapManyToOne += SetForeignKeyNameConvention;
            mapper.BeforeMapBag += SetForeignKeyNameConvention;
        }
    }
}