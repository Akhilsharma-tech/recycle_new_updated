using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ElectronicRecyclers.One800Recycling.Application.ViewModels
{
    public class NoteSummary
    {
        public int Id { get; set; }

        [AllowHtml]
        public string Text { get; set; }

        public string AccessLevel { get; set; }

        public DateTimeOffset ModifiedOn { get; set; }

        public string ModifiedBy { get; set; }
    }
}
