using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace ElectronicRecyclers.One800Recycling.Domain.Entities
{
    /// <summary>
    /// Enumeration 2.0.0 class was installed from nuget packaged 
    /// developed by Headspring
    /// </summary>
    [Serializable]
    [DebuggerDisplay("{DisplayName} - {Value}")]
    public abstract class Enumeration<TEnumeration> : Enumeration<TEnumeration, string>
        where TEnumeration : Enumeration<TEnumeration>
    {
        protected Enumeration(string value, string displayName)
            : base(value, displayName)
        {
        }

        public static TEnumeration FromString(string value)
        {
            return FromValue(value);
        }

        public static bool TryFromString(string listItemValue, out TEnumeration result)
        {
            return TryParse(listItemValue, out result);
        }
    }

    /// <summary>
    /// Enumeration 2.0.0 class was installed from nuget packaged 
    /// developed by Headspring
    /// </summary>
    [Serializable]
    [DebuggerDisplay("{DisplayName} - {Value}")]
    public abstract class Enumeration<TEnumeration, TValue> : IComparable<TEnumeration>, IEquatable<TEnumeration>
        where TEnumeration : Enumeration<TEnumeration, TValue>
        where TValue : IComparable
    {
        readonly string displayName;
        readonly TValue value;

        private static readonly Lazy<TEnumeration[]> enumerations =
            new Lazy<TEnumeration[]>(GetEnumerations);

        protected Enumeration(TValue value, string displayName)
        {
            this.value = value;
            this.displayName = displayName;
        }

        public TValue Value
        {
            get { return value; }
        }

        public string DisplayName
        {
            get { return displayName; }
        }

        public int CompareTo(TEnumeration other)
        {
            return Value.CompareTo(other.Value);
        }

        public override sealed string ToString()
        {
            return DisplayName;
        }

        public static TEnumeration[] GetAll()
        {
            return enumerations.Value;
        }

        private static TEnumeration[] GetEnumerations()
        {
            var enumerationType = typeof(TEnumeration);
            return enumerationType
                .GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly)
                .Where(info => enumerationType.IsAssignableFrom(info.FieldType))
                .Select(info => info.GetValue(null))
                .Cast<TEnumeration>()
                .ToArray();
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as TEnumeration);
        }

        public bool Equals(TEnumeration other)
        {
            return other != null && Value.Equals(other.Value);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public static bool operator ==(Enumeration<TEnumeration, TValue> left, Enumeration<TEnumeration, TValue> right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Enumeration<TEnumeration, TValue> left, Enumeration<TEnumeration, TValue> right)
        {
            return !Equals(left, right);
        }

        public static TEnumeration FromValue(TValue value)
        {
            return Parse(value, "value", item => item.Value.Equals(value));
        }

        public static TEnumeration Parse(string displayName)
        {
            return Parse(displayName, "display name", item => item.DisplayName == displayName);
        }

        static bool TryParse(Func<TEnumeration, bool> predicate, out TEnumeration result)
        {
            result = GetAll().FirstOrDefault(predicate);
            return result != null;
        }

        private static TEnumeration Parse(object value, string description, Func<TEnumeration, bool> predicate)
        {
            TEnumeration result;

            if (TryParse(predicate, out result)) 
                return result;

            var message = string.Format("'{0}' is not a valid {1} in {2}", value, description, typeof(TEnumeration));
            throw new ArgumentException(message, "value");
        }

        public static bool TryParse(TValue value, out TEnumeration result)
        {
            return TryParse(e => e.Value.Equals(value), out result);
        }

        public static bool TryParse(string displayName, out TEnumeration result)
        {
            return TryParse(e => e.DisplayName == displayName, out result);
        }
    }
}