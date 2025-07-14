namespace ElectronicRecyclers.One800Recycling.Domain.ServiceObjects
{
    public interface IServiceEnvironmentalOrganization
    {
        string Name { get; set; }

        string AddressLine1 { get; set; }

        string AddressLine2 { get; set; }

        string City { get; set; }

        string State { get; set; }

        string Region { get; set; }

        string PostalCode { get; set; }

        string Country { get; set; }

        double Latitude { get; set; }

        double Longitude { get; set; }

        string Telephone { get; set; }

        string Fax { get; set; }

        string WebsiteUrl { get; set; }

        string HoursOfOperation { get; set; }
    }
}
