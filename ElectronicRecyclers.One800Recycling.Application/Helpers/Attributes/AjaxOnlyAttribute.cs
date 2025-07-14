using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;


namespace ElectronicRecyclers.One800Recycling.Application.Helpers.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public sealed class AjaxOnlyAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var request = filterContext.HttpContext.Request;
            if (!request.Headers.ContainsKey("X-Requested-With") || request.Headers["X-Requested-With"] != "XMLHttpRequest")
            {
                filterContext.Result = new ContentResult
                {
                    Content = "Only Ajax calls are allowed.",
                    StatusCode = 404
                };
            }
        }
    }
}