using System;

namespace ElectronicRecyclers.One800Recycling.Domain.ValueObjects
{
    /// <summary>
    /// Country is immutable object.
    /// </summary>
    [Serializable]
    public class Country : LookupCode
    {
        protected Country() { }

        public Country(string name, string code, string description)
            : base(name, code, description) { }
    }
}