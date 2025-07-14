using ElectronicRecyclers.One800Recycling.Domain.Common;
using ElectronicRecyclers.One800Recycling.Domain.Exceptions;
using ElectronicRecyclers.One800Recycling.Domain.ValueObjects;
using System;

namespace ElectronicRecyclers.One800Recycling.Domain.Entities
{
    [Serializable]
    public class DismantlingProcess : DomainObject
    {
        public virtual string Name { get; set; }

        private decimal lossPercentage;

        public virtual decimal LossPercentageDuringRecycling
        {
            get { return lossPercentage; }
            set { lossPercentage = (value < 0) ? 0 : value; }
        }

        public virtual EnvironmentalImpact EnvironmentalImpact { get; set; }

        public virtual DismantlingProcessType Type { get; set; }

        protected DismantlingProcess()
        {
        }

        public DismantlingProcess(
            string name,
            EnvironmentalImpact environmentalImpact,
            decimal lossPercentage,
            DismantlingProcessType type)
        {
            Guard.Against<ArgumentNullException>(string.IsNullOrWhiteSpace(name), "Name is null.");
            Guard.Against<ArgumentNullException>(environmentalImpact == null, "Environmental impact is null.");
            Guard.Against<BusinessException>(lossPercentage < 0, "Loss percentage is invalid.");

            Name = name;
            EnvironmentalImpact = environmentalImpact;
            LossPercentageDuringRecycling = lossPercentage;
            Type = type;
        }

        public DismantlingProcess(
            string name,
            EnvironmentalImpact environmentalImpact,
            DismantlingProcessType type) : this(name, environmentalImpact, 0M, type)
        {
        }
    }
}