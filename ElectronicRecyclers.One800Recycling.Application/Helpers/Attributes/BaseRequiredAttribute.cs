using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ElectronicRecyclers.One800Recycling.Application.Helpers.Attributes
{
    public abstract class BaseRequiredAttribute : ValidationAttribute, IClientValidatable
    {
        protected readonly RequiredAttribute requiredAttribute = new RequiredAttribute();

        protected abstract string ValidationType { get; }

        public string DependentProperty { get; set; }
        public object TargetValue { get; set; }

        public BaseRequiredAttribute() { }

        public BaseRequiredAttribute(string dependentProperty, object targetValue)
        {
            DependentProperty = dependentProperty;
            TargetValue = targetValue;
        }

        private string BuildDependentPropertyId(ModelMetadata metadata, ViewContext context)
        {
            var dependentPropertyId = context
                .ViewData
                .TemplateInfo
                .GetFullHtmlFieldId(DependentProperty);

            var thisField = metadata.PropertyName + "_";
            if (dependentPropertyId.StartsWith(thisField))
                dependentPropertyId = dependentPropertyId.Substring(thisField.Length);

            return dependentPropertyId;
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(
            ModelMetadata metadata,
            ControllerContext context)
        {
            var rule = new ModelClientValidationRule()
            {
                ErrorMessage = FormatErrorMessage(metadata.GetDisplayName()),
                ValidationType = ValidationType
            };

            var dependentPropertyId = BuildDependentPropertyId(metadata, context as ViewContext);

            var targetValue = (TargetValue ?? "").ToString();
            if (TargetValue.GetType() == typeof(bool))
                targetValue = targetValue.ToLower();

            rule.ValidationParameters.Add("dependentproperty", dependentPropertyId);
            rule.ValidationParameters.Add("targetvalue", targetValue);

            yield return rule;
        }
    }
}