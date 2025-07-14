using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;


namespace ElectronicRecyclers.One800Recycling.Application.Attributes
{
    public class PremiumUsersOnlyAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext httpContext)
        {
            if (httpContext == null)
                throw new ArgumentNullException("httpContext");

            var user = httpContext.HttpContext.User;

            if (!user.Identity?.IsAuthenticated ?? true || !user.IsInRole("Administrator") || !user.IsInRole("Power User"))
            {
                httpContext.Result = new ForbidResult();
            }
        }
    }
}