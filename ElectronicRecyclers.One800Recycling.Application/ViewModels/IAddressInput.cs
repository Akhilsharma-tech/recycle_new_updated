using ElectronicRecyclers.One800Recycling.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicRecyclers.One800Recycling.Application.ViewModels
{
    public interface IAddressInput
    {
        string AddressLine1 { get; set; }
        string AddressLine2 { get; set; }
        string City { get; set; }
        string Region { get; set; }
        IEnumerable<State> States { get; set; }
        string SelectedStateCode { get; set; }
        string PostalCode { get; set; }
        double Latitude { get; set; }
        double Longitude { get; set; }
        IEnumerable<Country> Countries { get; set; }
        string SelectedCountryCode { get; set; }
    }
}
