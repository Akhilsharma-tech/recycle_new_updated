using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using ElectronicRecyclers.One800Recycling.Application.Common;

namespace ElectronicRecyclers.One800Recycling.Web.Helpers.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public sealed class HoursOfOperationAttribute : DataTypeAttribute, IClientValidatable
    {
        public HoursOfOperationAttribute() : base("businesshours")
        {
        }

        private static readonly string hoursRegex = HoursOfOperationValidator.HoursRegex;

        public string Regex
        {
            get { return hoursRegex; }
        }

        public override string FormatErrorMessage(string name)
        {
            if (ErrorMessage == null && ErrorMessageResourceName == null)
                ErrorMessage = "Business hours are invalid. " +
                               "Valid example: Mon-Fri 8:00-17:00, Sat Closed, Sun 11:00-20:30";

            return base.FormatErrorMessage(name);
        }

        public override bool IsValid(object value)
        {
            return value == null || HoursOfOperationValidator.Validate(value.ToString());
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(
            ModelMetadata metadata, 
            ControllerContext context)
        {
            var rule = new ModelClientValidationRegexRule(
                FormatErrorMessage(metadata.GetDisplayName()), 
                hoursRegex)
            {
                ValidationType = "businesshours"
            };

            yield return rule;
        }
    }
}