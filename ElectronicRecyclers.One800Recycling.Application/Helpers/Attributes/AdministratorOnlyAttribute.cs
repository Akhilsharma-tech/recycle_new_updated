using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace ElectronicRecyclers.One800Recycling.Application.Helpers.Attributes
{
    public class AdministratorOnlyAttribute :  Attribute , IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext httpContext)
        {
            if (httpContext == null)
                throw new ArgumentNullException("httpContext");

            var user = httpContext.HttpContext.User;

            if (!user.Identity?.IsAuthenticated ?? true || !user.IsInRole("Administrator"))
            {
                httpContext.Result = new ForbidResult(); 
            }
        }
    }
}