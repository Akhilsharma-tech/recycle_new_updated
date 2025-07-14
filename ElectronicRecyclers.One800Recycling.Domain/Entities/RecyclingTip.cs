using System;
using ElectronicRecyclers.One800Recycling.Domain.Common;
using ElectronicRecyclers.One800Recycling.Domain.Exceptions;

namespace ElectronicRecyclers.One800Recycling.Domain.Entities
{
    public class RecyclingTip : DomainObject
    {
        public virtual string Title { get; set; }

        public virtual string Description { get; set; }

        public virtual string ImageName { get; set; }

        public virtual int Number { get; protected set; }

        protected RecyclingTip() { }

        public RecyclingTip(string title, string description, string imageName, int number)
        {
            Guard.Against<ArgumentNullException>(string.IsNullOrWhiteSpace(title), "Title is null.");
            Guard.Against<ArgumentNullException>(string.IsNullOrWhiteSpace(description), "Description is null.");
            Guard.Against<ArgumentNullException>(string.IsNullOrWhiteSpace(imageName), "Image name is null.");
            Guard.Against<BusinessException>(number <= 0, "Number is invalid.");

            Title = title;
            Description = description;
            ImageName = imageName;
            Number = number;
        }
    }
}