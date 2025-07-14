using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ElectronicRecyclers.One800Recycling.Domain.Common;
using ElectronicRecyclers.One800Recycling.Domain.ValueObjects;

namespace ElectronicRecyclers.One800Recycling.Domain.Entities
{
    [Serializable]
    public class Material : DomainObject
    {
        protected Material() {}

        public Material(string name, string description, bool isEnabled)
        {
            Name = name;
            Description = description;
            IsEnabled = isEnabled;
        }

        public Material(string name, string description) 
            : this(name, description, true)
        {
            
        }

        public virtual string Name { get; set; }

        public virtual string Description { get; set; }

        public virtual bool IsEnabled { get; set; }

        private readonly ICollection<MaterialCategory> categories = 
            new Collection<MaterialCategory>();

        public virtual void AddCategory(MaterialCategory category)
        {
            if (category == null)
                return;

            if (categories.Contains(category) == false)
                categories.Add(category);
        }

        public virtual bool RemoveCategory(MaterialCategory category)
        {
            if (category == null)
                return false;

            return categories.Contains(category) && categories.Remove(category);
        }

        public virtual MaterialCategory GetCategory(int id)
        {
            return categories.FirstOrDefault(c => c.Id == id);
        }

        public virtual bool RemoveCategory(int id)
        {
            return RemoveCategory(GetCategory(id));
        }

        public virtual IEnumerable<MaterialCategory> GetCategories() 
        {
            return categories;
        }

        private readonly ICollection<string> searchKeywords = new Collection<string>();

        public virtual void AddSearchKeyword(string keyword)
        {
            if (string.IsNullOrEmpty(keyword))
                return;

            keyword = keyword.Trim().ToUpper();

            if (searchKeywords.Contains(keyword) == false)
                searchKeywords.Add(keyword);
        }

        public virtual bool RemoveSearchKeyword(string keyword)
        {
            if (string.IsNullOrEmpty(keyword))
                return false;

            keyword = keyword.Trim().ToUpper();
            return searchKeywords.Contains(keyword) 
                && searchKeywords.Remove(keyword);
        }

        public virtual IEnumerable<string> GetSearchKeywords()
        {
            return searchKeywords;
        }

        private readonly ICollection<MaterialComposition> compositions = 
            new Collection<MaterialComposition>();

        public virtual void AddComposition(MaterialComponent component, decimal percentage)
        {
            if (component != null 
                && compositions.FirstOrDefault(c => c.MaterialComponent == component) == null)
                compositions.Add(new MaterialComposition(this, component, percentage));
        }

        public virtual bool RemoveComposition(MaterialComposition composition)
        {
            return composition != null
                   && compositions.Contains(composition)
                   && compositions.Remove(composition);
        }

        public virtual MaterialComposition GetComposition(int id)
        {
            return compositions.FirstOrDefault(c => c.Id == id);
        }

        public virtual bool RemoveComposition(int id)
        {
            return RemoveComposition(GetComposition(id));
        }

        public virtual IEnumerable<MaterialComposition> GetCompositions()
        {
            return compositions;
        }

        private readonly ICollection<MaterialProductDismantlingProcess> processes =
           new Collection<MaterialProductDismantlingProcess>();

        public virtual void AddProcess(ProductDismantlingProcess process, decimal percentage)
        {
            if (process != null
                  && processes.FirstOrDefault(p => p.ProductDismantlingProcess == process) == null)
                processes.Add(new MaterialProductDismantlingProcess(
                                                    this, 
                                                    process, 
                                                    percentage));
        }

        public virtual bool RemoveProcess(MaterialProductDismantlingProcess process)
        {
            return process != null
                   && processes.Contains(process)
                   && processes.Remove(process);
        }

        public virtual MaterialProductDismantlingProcess GetProcess(int id)
        {
            return processes.FirstOrDefault(p => p.Id == id);
        }

        public virtual bool RemoveProcess(int id)
        {
            return RemoveProcess(GetProcess(id));
        }

        public virtual IEnumerable<MaterialProductDismantlingProcess> GetProcesses()
        {
            return processes;
        }

        public virtual RecyclingClimateChangeImpactEquivalent GetRecyclingClimateChangeImpactEquivalent(decimal weight)
        {
            return (compositions.Any())
                ? new RecyclingClimateChangeImpactEquivalent(
                    new RecyclingClimateChangeImpact(this, weight))
                : null;
        }

        public virtual RecyclingResourceDepletionImpactEquivalent GetRecyclingResourceDepletionImpactEquivalent(decimal weight)
        {
            return (compositions.Any())
                ? new RecyclingResourceDepletionImpactEquivalent(
                    new RecyclingResourceDepletionImpact(this, weight))
                : null;
        }

        public virtual RecyclingWaterWithdrawalImpactEquivalent GetRecyclingWaterWithdrawalImpactEquivalent(decimal weight)
        {
            return (compositions.Any())
                ? new RecyclingWaterWithdrawalImpactEquivalent(
                    new RecyclingWaterWithdrawalImpact(this, weight))
                : null;
        }

        public virtual RecyclingClimateChangeImpactEquivalent GetRecyclingClimateChangeImpactEquivalent()
        {
            return GetRecyclingClimateChangeImpactEquivalent(1M);
        }

        public virtual RecyclingResourceDepletionImpactEquivalent GetRecyclingResourceDepletionImpactEquivalent()
        {
            return GetRecyclingResourceDepletionImpactEquivalent(1M);
        }

        public virtual RecyclingWaterWithdrawalImpactEquivalent GetRecyclingWaterWithdrawalImpactEquivalent()
        {
            return GetRecyclingWaterWithdrawalImpactEquivalent(1M);
        }
    }
}