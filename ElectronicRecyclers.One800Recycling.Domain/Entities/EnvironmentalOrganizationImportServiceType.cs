namespace ElectronicRecyclers.One800Recycling.Domain.Entities
{
    public class EnvironmentalOrganizationImportServiceType : Enumeration<EnvironmentalOrganizationImportServiceType, int>
    {
        public static readonly EnvironmentalOrganizationImportServiceType BestBuyService = 
            new EnvironmentalOrganizationImportServiceType(1, "Best Buy Service");

        public EnvironmentalOrganizationImportServiceType(int value, string displayName) 
            : base(value, displayName)
        {
        }
    }
}