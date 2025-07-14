using ElectronicRecyclers.One800Recycling.Domain.Common;
using System;
using System.Threading;

namespace ElectronicRecyclers.One800Recycling.Domain.ValueObjects
{
    /// <summary>
    /// Name is immutable object.
    /// </summary>
    [Serializable]
    public class Name : IEquatable<Name>
    {
        public virtual string FirstName { get; protected set; }

        public virtual string MiddleName { get; protected set; }

        public virtual string LastName { get; protected set; }

        protected Name() { }

        public Name(string firstName, string middleName, string lastName)
        {
            Guard.Against<ArgumentNullException>(string.IsNullOrEmpty(firstName), "First name is required.");
            Guard.Against<ArgumentNullException>(middleName == null, "Middle name is null.");
            Guard.Against<ArgumentNullException>(string.IsNullOrEmpty(lastName), "Last name is required.");

            FirstName = firstName.ToUpper();
            MiddleName = middleName.ToUpper();
            LastName = lastName.ToUpper();
        }

        public Name(string firstName, string lastName)
            : this(firstName, string.Empty, lastName)
        {
        }

        public static Name EmptyName()
        {
            return new Name(" ", string.Empty, " ");
        }

        public override int GetHashCode()
        {
            return FirstName.GetHashCode()
                ^ MiddleName.GetHashCode()
                ^ LastName.GetHashCode();
        }

        public bool Equals(Name other)
        {
            if (other == null)
                return false;

            return FirstName.Equals(other.FirstName)
                && (
                        (string.IsNullOrEmpty(MiddleName) && string.IsNullOrEmpty(other.MiddleName)) 
                        || (MiddleName != null && MiddleName.Equals(other.MiddleName))
                   )
                && LastName.Equals(other.LastName);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Name);
        }

        public static bool operator ==(Name objA, Name objB) 
        {
            return Equals(objA, objB);
        }

        public static bool operator !=(Name objA, Name objB)
        {
            return !Equals(objA, objB);
        }

        public override string ToString()
        {
            return (string.IsNullOrWhiteSpace(MiddleName))
                ? string.Format("{0} {1}", FirstName, LastName)
                : string.Format("{0} {1} {2}", FirstName, MiddleName, LastName);
        }

        public string ToTitleCase()
        {
            return Thread
                .CurrentThread
                .CurrentCulture
                .TextInfo
                .ToTitleCase(ToString().ToLower());
        }
    }
}