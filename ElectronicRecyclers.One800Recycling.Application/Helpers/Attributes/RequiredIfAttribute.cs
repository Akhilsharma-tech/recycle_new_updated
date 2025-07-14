using ElectronicRecyclers.One800Recycling.Application.Helpers.Attributes;
using System.ComponentModel.DataAnnotations;


namespace ElectronicRecyclers.One800Recycling.Application.Helpers.Attributes
{
    public class RequiredIfAttribute : BaseRequiredAttribute
    {
        public RequiredIfAttribute(string dependentProperty, object targetValue)
            : base(dependentProperty, targetValue)
        {
        }

        protected override string ValidationType
        {
            get { return "requiredif"; }
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var field = validationContext
                .ObjectInstance
                .GetType()
                .GetProperty(DependentProperty);

            if (field != null)
            {
                var dependentValue = field.GetValue(validationContext.ObjectInstance, null);
                
                if ((dependentValue == null && TargetValue == null) ||
                    (dependentValue != null && dependentValue.Equals(TargetValue)))
                {
                    if (requiredAttribute.IsValid(value) == false)
                        return new ValidationResult(ErrorMessage, new[] { validationContext.MemberName });
                }
            }

            return ValidationResult.Success;
        }
    }
}