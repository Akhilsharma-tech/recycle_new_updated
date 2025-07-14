using System;

namespace ElectronicRecyclers.One800Recycling.Domain.Entities
{
    [Serializable]
    public class Recycler : EnvironmentalOrganization
    {
        public Recycler() { }

        public Recycler(Recycler recycler) : base(recycler)
        {
            IsDedicatedRecycler = recycler.IsDedicatedRecycler;
        }

        public virtual bool IsDedicatedRecycler { get; set; }

        public override object Copy()
        {
            return new Recycler(this);
        }
    }
}