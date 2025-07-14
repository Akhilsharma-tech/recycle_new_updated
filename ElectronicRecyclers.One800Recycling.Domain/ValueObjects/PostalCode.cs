using ElectronicRecyclers.One800Recycling.Domain.Common;
using System;

namespace ElectronicRecyclers.One800Recycling.Domain.ValueObjects
{
    /// <summary>
    /// PostalCode is immutable object.
    /// </summary>
    [Serializable]
    public class PostalCode : DomainObject, IEquatable<PostalCode>
    {
        protected PostalCode() { }

        public virtual string Code { get; protected set; }
        public virtual string Region { get; protected set; }
        public virtual string City { get; protected set; }
        
        private string state;
        public virtual string State
        {
            get { return state; }
            protected set { state = value ?? string.Empty; }
        }

        public virtual string Country { get; protected set; }
        public virtual double Latitude { get; protected set; }
        public virtual double Longitude { get; protected set; }

        public PostalCode(
            string code,
            string city,
            string region, 
            string state,
            string country, 
            double latitude, 
            double longitude)
        {
            
            Guard.Against<ArgumentNullException>(string.IsNullOrWhiteSpace(code), "Code is required.");
            Guard.Against<ArgumentNullException>(string.IsNullOrWhiteSpace(region), "Region is required.");
            Guard.Against<ArgumentNullException>(string.IsNullOrWhiteSpace(city), "City is required.");
            Guard.Against<ArgumentNullException>(string.IsNullOrWhiteSpace(country), "Country is required.");

            Code = code;
            City = city;
            Region = region;
            State = state;
            Country = country;
            Latitude = latitude;
            Longitude = longitude;
        }

        public PostalCode(
            string code,
            string city, 
            string region, 
            string country, 
            double latitude, 
            double longitude)
            : this(code, city, region, string.Empty, country, latitude, longitude)
        {
        }

        public override int GetHashCode()
        {
            return Code.GetHashCode()
                ^ Region.GetHashCode()
                ^ City.GetHashCode()
                ^ State.GetHashCode()
                ^ Country.GetHashCode()
                ^ Latitude.GetHashCode()
                ^ Longitude.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as PostalCode);
        }

        public virtual bool Equals(PostalCode other)
        {
            if (other == null)
                return false;

            return Code.Equals(other.Code)
                && Region.Equals(other.Region)
                && City.Equals(other.City)
                && State.Equals(other.State)
                && Country.Equals(other.Country)
                && Latitude.Equals(other.Latitude)
                && Longitude.Equals(other.Longitude);
        }

        public static bool operator ==(PostalCode left, PostalCode right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(PostalCode left, PostalCode right)
        {
            return !Equals(left, right);
        }
    }
}