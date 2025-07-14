using System.Linq;
using System.Web.Mvc;

namespace ElectronicRecyclers.One800Recycling.Web.Helpers.Engines
{
    public class RecyclerViewEngine : RazorViewEngine
    {
        public RecyclerViewEngine()
        {
            PartialViewLocationFormats = PartialViewLocationFormats
                .Union(new[]
                {
                    "~/Areas/Admin/Views/{1}/{0}.cshtml",
                    "~/Areas/Admin/Views/Shared/{0}.cshtml"
                })
                .ToArray();
        }
    }
}