using System.Collections.Generic;
using System.Linq;
using ElectronicRecyclers.One800Recycling.Application.ViewModels;
using ElectronicRecyclers.One800Recycling.Domain.ServiceObjects;
using ElectronicRecyclers.One800Recycling.Domain.ValueObjects;
using ElectronicRecyclers.One800Recycling.Web.ViewModels;

namespace ElectronicRecyclers.One800Recycling.Application.AutoMapper.Profiles.Resolvers
{
    public class AddressResolver
    {
        public static Address Resolve(IAddressInput input)
        {
            return new Address(
                input.AddressLine1,
                input.AddressLine2,
                input.City,
                input.Region,
                input.SelectedStateCode,
                input.PostalCode,
                input.SelectedCountryCode,
                input.Latitude,
                input.Longitude);
        }

        public static Address Resolve(IServiceEnvironmentalOrganization organization)
        {
            return new Address(
                organization.AddressLine1,
                organization.AddressLine2,
                organization.City,
                organization.Region,
                organization.State,
                organization.PostalCode,
                organization.Country,
                organization.Latitude,
                organization.Longitude);
        }

        public static Address Resolve(SmartyStreetsResponseJson input)
        {
            const string country = "US";

            return new Address(
                input.DeliveryAddressLine1,
                input.DeliveryAddressLine2,
                input.Address.City,
                input.AddressGeocoding.CountyName,
                input.Address.State,
                input.Address.Zip,
                country, 
                input.AddressGeocoding.Latitude, 
                input.AddressGeocoding.Longitude);
        }

        private static string GetAddressComponentValue(
            IEnumerable<GoogleGeocodingResult.AddressComponent> components, 
            string type)
        {
            var component = components.FirstOrDefault(a => a.Types.Contains(type));
            return component == null ? string.Empty : component.ShortName;
        }

        private static string TrimPostalCodeIfLongerThan20Chars(string postalCode)
        {
            return (postalCode.ToCharArray().Length > 19)
                ? postalCode.Substring(0, 19)
                : postalCode;
        }

        public static Address Resolve(GoogleGeocodingResult input)
        {
            var addressComponents = input.AddressComponents;
            if (addressComponents == null || addressComponents.Count == 1)
                return Address.USEmptyAddress();

            var location = input.AddressGeometry.Location;
            var addressLine = string.Format(
                "{0} {1} {2}",
                GetAddressComponentValue(addressComponents, "street_number"),
                GetAddressComponentValue(addressComponents, "route"),
                GetAddressComponentValue(addressComponents, "subpremise"));

            var country = GetAddressComponentValue(addressComponents, "country");
            var state = country == "US"
                ? GetAddressComponentValue(addressComponents, "administrative_area_level_1")
                : string.Empty;

            var postalCode = GetAddressComponentValue(addressComponents, "postal_code");
            postalCode = TrimPostalCodeIfLongerThan20Chars(postalCode);

            return new Address(
                addressLine,
                GetAddressComponentValue(addressComponents, "neighborhood"),
                GetAddressComponentValue(addressComponents, "locality"),
                GetAddressComponentValue(addressComponents, "administrative_area_level_2"),
                state,
                postalCode,
                country,
                location.Latitude,
                location.Longitude);
        }
    }
}