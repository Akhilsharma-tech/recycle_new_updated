using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicRecyclers.One800Recycling.Application.ViewModels
{
    public class ServiceConsumerInput
    {
        [HiddenInput]
        public int Id { get; set; }

        [Url]
        [Required]
        [StringLength(255)]
        [Display(Name = "Website Address")]
        [DataType(DataType.Url)]
        public string WebsiteUrl { get; set; }

        [Required]
        [StringLength(80)]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        public bool IsNewConsumer()
        {
            return Id == 0;
        }
    }
}
