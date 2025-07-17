using AutoMapper;
using ElectronicRecyclers.One800Recycling.Application.Helpers.Attributes;
using ElectronicRecyclers.One800Recycling.Web.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicRecyclers.One800Recycling.Application.ViewModels
{
    public class RecyclingTipInput : ViewModel
    {
        private static IConfiguration _configuration;
        public static void Configure(IConfiguration configuration)
        {
            _configuration = configuration;

        }
        private static string GetBlobStorageUrl()
        {
            return _configuration["RecyclerStorageImageDirUrl"];
        }

        [HiddenInput]
        public int Id { get; set; }

        [RequiredIf("ImageName", "", ErrorMessage = "Image file is required.")]
        [Display(Name = "Image")]
        [DataType(DataType.Upload)]
        public IFormFile ImageFile { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [Required]
        [StringLength(400)]
        [Display(Name = "Description")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        private string imageName;
        [HiddenInput]
        public string ImageName
        {
            get { return (ImageFile == null) ? imageName : ImageFile.FileName; }
            set { imageName = value; }
        }

        private string imageUrl;
        [HiddenInput]
        public string ImageUrl
        {
            get
            {
                return string.IsNullOrEmpty(imageUrl)
                    ? string.Empty
                    : string.Format("{0}{1}", GetBlobStorageUrl(), imageUrl);
            }
            set
            {
                imageUrl = value;
            }
        }

        public bool IsNewRecyclingTip()
        {
            return Id == 0;
        }
    }
}
