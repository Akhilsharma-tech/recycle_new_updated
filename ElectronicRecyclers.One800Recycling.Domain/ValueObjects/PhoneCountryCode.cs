using System;

namespace ElectronicRecyclers.One800Recycling.Domain.ValueObjects
{
    /// <summary>
    /// PhoneCountryCode is immutable object.
    /// </summary>
    [Serializable]
    public class PhoneCountryCode : LookupCode
    {
        protected PhoneCountryCode() { }

        public PhoneCountryCode(string name, string code, string description)
            : base(name, code, description) { }
    }
}