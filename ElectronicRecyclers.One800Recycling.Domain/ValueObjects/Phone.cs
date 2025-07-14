
using ElectronicRecyclers.One800Recycling.Domain.Common;
using PhoneNumbers;
using System;

namespace ElectronicRecyclers.One800Recycling.Domain.ValueObjects
{
    /// <summary>
    /// Phone is immutable object.
    /// </summary>
    [Serializable]
    public class Phone : IEquatable<Phone>
    {
        protected Phone() { }

        public virtual string CountryCodeSource { get; protected set; }
        public virtual int CountryCode { get; protected set; }
        public virtual ulong Number { get; protected set; }
        public virtual string Extension { get; protected set; }

        /// <summary>
        /// Converts, validates, and formats string representation of phone number to its Phone equivalent.
        /// </summary>
        /// <param name="phoneCountryCode">Phone country code such as US, CA, GB.</param>
        /// <param name="phoneNumber">Phone number.</param>
        /// <exception cref="ArgumentException">If phone countryCode or number is empty or null.</exception>
        /// <exception cref="NumberParseException">If phone number is invalid.</exception>
        public static Phone Parse(string phoneCountryCode, string phoneNumber)
        {
            Guard.Against<ArgumentException>(
               string.IsNullOrEmpty(phoneCountryCode),
               "Phone country code is null.");

            Guard.Against<ArgumentException>(
                string.IsNullOrEmpty(phoneNumber),
                "Phone number is null.");

            if (phoneCountryCode == " " && phoneNumber == " ")
                return EmptyPhone();

            var parsedPhoneNumber = PhoneNumberUtil
                .GetInstance()
                .Parse(phoneNumber, phoneCountryCode);
           
            return new Phone
            {
                CountryCodeSource = phoneCountryCode.ToUpper(),
                CountryCode = parsedPhoneNumber.CountryCode,
                Number = parsedPhoneNumber.NationalNumber,
                Extension = parsedPhoneNumber.Extension
            };
        }

        public static bool TryParse(string phoneCountryCode, string phoneNumber, out Phone result)
        {
            result = EmptyPhone();
           
            try
            {
                result = Parse(phoneCountryCode, phoneNumber);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static Phone EmptyPhone()
        {
            return new Phone
            {
                CountryCodeSource = " ",
                CountryCode = 0,
                Number = 0,
                Extension = " "
            };
        }

        public override int GetHashCode()
        {
            return CountryCodeSource.GetHashCode()
                ^ CountryCode.GetHashCode()
                ^ Number.GetHashCode()
                ^ Extension.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Phone);
        }

        public virtual bool Equals(Phone other)
        {
            if (other == null) 
                return false;

            return CountryCodeSource.Equals(other.CountryCodeSource)
                && CountryCode.Equals(other.CountryCode)
                && Number.Equals(other.Number)
                && Extension.Equals(other.Extension);
        }

        public static bool operator ==(Phone left, Phone right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Phone left, Phone right)
        {
            return !Equals(left, right);
        }

        public override string ToString()
        {
            if (this == EmptyPhone())
                return string.Empty;

            var number = Number.ToString();

            if (CountryCode == 1 && number.Length == 10)
                return string.Format(
                    "({0}) {1}-{2}",
                    number.Substring(0, 3),
                    number.Substring(3, 3),
                    number.Substring(6, 4));

            return number;
        }
    }
}