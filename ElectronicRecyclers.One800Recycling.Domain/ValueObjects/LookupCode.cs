using ElectronicRecyclers.One800Recycling.Domain.Common;
using ElectronicRecyclers.One800Recycling.Domain.Exceptions;
using System;

namespace ElectronicRecyclers.One800Recycling.Domain.ValueObjects
{
    /// <summary>
    /// LookupCode is immutable object.
    /// </summary>
    [Serializable]
    public abstract class LookupCode : DomainObject, IEquatable<LookupCode>, ILookupCode
    {
        protected LookupCode() { }

        public virtual string Name { get; protected set; }
        public virtual string Code { get; protected set; }
        public virtual string Description { get; protected set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="code"></param>
        /// <param name="description"></param>
        protected LookupCode(string name, string code, string description)
        {
            Guard.Against<ArgumentException>(string.IsNullOrWhiteSpace(name), "Name is required.");
            Guard.Against<ArgumentException>(string.IsNullOrWhiteSpace(code), "Code is required.");
            Guard.Against<BusinessException>(code.Contains(" "), "Code contains spaces.");
            Guard.Against<ArgumentException>(string.IsNullOrWhiteSpace(description), "Description is required.");

            Name = name;
            Code = code.ToUpper();
            Description = description;
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode()
                ^ Code.GetHashCode()
                ^ Description.GetHashCode();
        }

        public virtual bool Equals(LookupCode other)
        {
            if (other == null)
                return false;

            return Name.Equals(other.Name)
                && Code.Equals(other.Code)
                && Description.Equals(other.Description);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as LookupCode);
        }

        public static bool operator ==(LookupCode objA, LookupCode objB)
        {
            return Equals(objA, objB);
        }

        public static bool operator !=(LookupCode objA, LookupCode objB)
        {
            return !Equals(objA, objB);
        }

        public override string ToString()
        {
            return Name;
        }
    }
}