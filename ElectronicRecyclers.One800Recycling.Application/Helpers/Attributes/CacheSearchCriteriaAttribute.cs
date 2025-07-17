using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using ElectronicRecyclers.One800Recycling.Application.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ElectronicRecyclers.One800Recycling.Web.Helpers.Attributes
{
    public class CacheSearchCriteriaAttribute : ActionFilterAttribute
    {
        private static readonly IEnumerable<string> searchActions = new List<string> { "Index" };

        private static string CreateKey(string controller, string action)
        {
            return string.Format("UrlQuery_{0}_{1}", controller, action);
        }

        private static void ClearTempData(IDictionary<string, object> tempData, string controller)
        {
            var keys = tempData.Keys.ToArray();
            keys.ForEach(k =>
            {
                if (Regex.IsMatch(k, string.Format("^UrlQuery_(?!{0})", controller)))
                    tempData.Remove(k);
            });
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            if (filterContext.HttpContext.User.Identity?.IsAuthenticated == false)
                return;

            var routeData = filterContext.RouteData;
            var controller = routeData.Values["controller"].ToString();
            var action = routeData.Values["action"].ToString();
            var key = CreateKey(controller, action);

            var request = filterContext.HttpContext.Request;
            var url = $"{request.Path}{request.QueryString}";
            if (filterContext.Controller is Controller controllerBase)
            {
                var tempData = controllerBase.TempData;
                if (string.IsNullOrEmpty(request.QueryString.Value) == false && searchActions.Any(a => a == action))
                {
                     tempData[key] = url;
                }
                else
                {
                    if (tempData.ContainsKey(key))
                        filterContext.Result = new RedirectResult(tempData[key].ToString());
             
                    ClearTempData(tempData, controller);
                }
            }
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);

            if (filterContext.HttpContext.User?.Identity?.IsAuthenticated == false)
                return;

            var result = filterContext.Result as RedirectToRouteResult;
            if (result == null)
                return;
            if (filterContext.Controller is Controller controllerBase)
            {
                var key = CreateKey(
                result.RouteValues["controller"].ToString(),
                result.RouteValues["action"].ToString());

                var tempData = controllerBase.TempData;
                if (tempData.ContainsKey(key))
                    filterContext.Result = new RedirectResult(tempData[key].ToString());
            }

        }
    }
}