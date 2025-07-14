using ElectronicRecyclers.One800Recycling.Domain.Common;
using System;

namespace ElectronicRecyclers.One800Recycling.Domain.Entities
{
    [Serializable]
    public class Note : DomainObject
    {
        protected Note() { }

        public Note(string text, AccessLevel level)
        {
            Text = text;
            AccessLevel = level;
        }

        public virtual string Text { get; set; }

        public virtual AccessLevel AccessLevel { get; set; }
    }
}