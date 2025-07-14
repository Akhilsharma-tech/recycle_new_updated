using Microsoft.AspNetCore.Mvc.Routing;


namespace ElectronicRecyclers.One800Recycling.Application.Common
{
    public static class UrlHelperExtensions
    {
        private const string ScriptDir = "Scripts";

        public static string Script(this UrlHelper urlHelper, string fileName)
        {
            if (!fileName.EndsWith(".js"))
                fileName += ".js";

            return urlHelper.Content(string.Format("~/{0}/{1}", ScriptDir, fileName));
        }
    }
}