using System;
using System.Web.Mvc;
using ElectronicRecyclers.One800Recycling.Application.Helpers;
using ElectronicRecyclers.One800Recycling.Web.ViewModels;

namespace ElectronicRecyclers.One800Recycling.Web.Helpers.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public class PagedListMultiRowSelectAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var model = filterContext
                .Controller
                .ViewData
                .Model as PagedListMultiRowSelectViewModel;

            if (model == null)
                return;

            new PagedListMultiRowSelector(model)
                .SelectRows();
        }
    }
}