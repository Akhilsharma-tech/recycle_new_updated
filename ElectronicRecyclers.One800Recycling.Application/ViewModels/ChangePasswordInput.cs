using ElectronicRecyclers.One800Recycling.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicRecyclers.One800Recycling.Application.ViewModels
{
    public class ChangePasswordInput : ViewModel
    {
        [HiddenInput]
        public int Id { get; set; }

        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 6,
            ErrorMessage = "The password must be at least {2} characters long.")]
        [Display(Name = "New Password")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Display(Name = "Confirm New Password")]
        [System.ComponentModel.DataAnnotations.Compare("NewPassword",
            ErrorMessage = "The password and confirmation password do not match.")]
        [DataType(DataType.Password)]
        public string ConfirmNewPassword { get; set; }
    }
}
