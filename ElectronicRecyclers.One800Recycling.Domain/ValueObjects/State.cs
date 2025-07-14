using System;

namespace ElectronicRecyclers.One800Recycling.Domain.ValueObjects
{
    /// <summary>
    /// State is immutable object.
    /// </summary>
    [Serializable]
    public class State : LookupCode
    {
        protected State() { }

        public State(string name, string code, string description) 
            : base(name, code, description) { }
    }
}