using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace ElectronicRecyclers.One800Recycling.Application.Helpers.Attributes
{
    public class SystemUsersOnlyAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            var user = context.HttpContext.User;

            if (!user.Identity?.IsAuthenticated ?? true ||
                !(user.IsInRole("Administrator") || user.IsInRole("Power User") || user.IsInRole("Editor")))
            {
                context.Result = new ForbidResult();
            }
        }
    }
}
