using System;
using System.Text;
using System.Diagnostics;
using Newtonsoft.Json;
using ElectronicRecyclers.One800Recycling.Domain.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace ElectronicRecyclers.One800Recycling.Web.Helpers.Results
{
    public class JsonWebResult : ActionResult
    {
        public JsonWebResult() { }

        public JsonWebResult(object responseBody)
        {
            ResponseBody = responseBody;
        }

        public object ResponseBody { get; set; }

        public string ContentType { get; set; }

        public Encoding ContentEnconding { get; set; }

        public Formatting Formatting
        {
            get { return Debugger.IsAttached ? Formatting.Indented : Formatting.None; }
        }

        public override void ExecuteResult(ActionContext context)
        {
            Guard.Against<ArgumentNullException>(context == null, "Context is null.");

            var response = context.HttpContext.Response;

            response.ContentType = !string.IsNullOrEmpty(ContentType) 
                ? ContentType 
                : "application/json";
            
            if (ResponseBody != null)
                response.WriteAsync(JsonConvert.SerializeObject(ResponseBody, Formatting));

        }
    }
}