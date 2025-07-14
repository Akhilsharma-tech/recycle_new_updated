using System;
using ElectronicRecyclers.One800Recycling.Domain.Common;

namespace ElectronicRecyclers.One800Recycling.Domain.ValueObjects
{
    /// <summary>
    /// Address is immutable object.
    /// </summary>
    [Serializable]
    public class Address : IEquatable<Address>
    {
        protected Address() { }

        public virtual string AddressLine1 { get; protected set; }

        private string addressLine2;
        public virtual string AddressLine2
        {
            get { return addressLine2; }
            protected set { addressLine2 = value ?? string.Empty; }
        }

        public virtual string City { get; protected set; }

        private string region;
        public virtual string Region
        {
            get { return region; }
            protected set { region = value ?? string.Empty; }
        }

        private string state;
        public virtual string State
        {
            get { return state ?? string.Empty; }
            protected set { state = value ?? string.Empty; }
        }

        public virtual string PostalCode { get; protected set; }

        public virtual string Country { get; protected set; }

        public virtual double Latitude { get; protected set; }

        public virtual double Longitude { get; protected set; }

        public Address(
            string addressLine1, 
            string addressLine2, 
            string city, 
            string region, 
            string state, 
            string postalCode, 
            string country,
            double latitude,
            double longitude)
        {
            Guard.Against<ArgumentException>(
                string.IsNullOrWhiteSpace(country), 
                "Country is required.");

            AddressLine1 = addressLine1;
            AddressLine2 = addressLine2;
            City = city;
            Region = region;
            State = state;
            PostalCode = postalCode;
            Country = country;
            Latitude = latitude;
            Longitude = longitude;
        }

        private static double ConvertStringToDouble(string value)
        {
            double result = 0;
            double.TryParse(value, out result);
            return result;
        }

        public Address(
            string addressLine1,
            string addressLine2,
            string city,
            string region,
            string state,
            string postalCode,
            string country,
            string latitude,
            string longitude)
            : this(
            addressLine1,
            addressLine2,
            city,
            region,
            state,
            postalCode,
            country,
            ConvertStringToDouble(latitude),
            ConvertStringToDouble(longitude))
        {
            
        }

        public Address(
            string addressLine1,
            string addressLine2,
            string city, 
            string state, 
            string postalCode, 
            string country) 
            : this(
            addressLine1,
            addressLine2, 
            city, 
            string.Empty, 
            state, 
            postalCode, 
            country, 
            0, 
            0) { }

        public static Address USEmptyAddress()
        {
            return EmptyAddress("US");
        }

        public static Address EmptyAddress(string country) 
        {
            return new Address(
                string.Empty, 
                string.Empty, 
                string.Empty, 
                string.Empty, 
                string.Empty, 
                string.Empty, 
                country, 
                0, 
                0);
        }

        public bool IsEmpty()
        {
            return Equals(EmptyAddress(Country));
        }

        public override int GetHashCode()
        {
            return AddressLine1.GetHashCode()
                ^ AddressLine2.GetHashCode()
                ^ City.GetHashCode()
                ^ Region.GetHashCode()
                ^ State.GetHashCode()
                ^ PostalCode.GetHashCode()
                ^ Country.GetHashCode()
                ^ Latitude.GetHashCode()
                ^ Longitude.GetHashCode();
        }

        public bool Equals(Address other)
        {
            return AddressLine1.Equals(other.AddressLine1)
                && AddressLine2.Equals(other.AddressLine2)
                && City.Equals(other.City)
                && Region.Equals(other.Region)
                && State.Equals(other.State)
                && Country.Equals(other.Country)
                && Latitude.Equals(other.Latitude)
                && Longitude.Equals(other.Longitude);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Address);
        }

        public static bool operator ==(Address objA, Address objB)
        {
            return Equals(objA, objB);
        }

        public static bool operator !=(Address objA, Address objB)
        {
            return !Equals(objA, objB);
        }

        public override string ToString()
        {
            return string.Format("{0}, {1}, {2} {3}, {4}", 
                AddressLine1, 
                City, 
                string.IsNullOrWhiteSpace(State) == false ? State : Region, 
                PostalCode, 
                Country);
        }
    }
}