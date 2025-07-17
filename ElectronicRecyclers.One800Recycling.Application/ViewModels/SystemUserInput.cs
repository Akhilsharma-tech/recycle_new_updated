using ElectronicRecyclers.One800Recycling.Application.Helpers.Attributes;
using ElectronicRecyclers.One800Recycling.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicRecyclers.One800Recycling.Application.ViewModels
{
    public class SystemUserInput : ViewModel
    {
        [HiddenInput]
        public int Id { get; set; }

        [Required]
        [StringLength(40)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(60)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [StringLength(80)]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [RequiredIf("Id", 0)]
        [StringLength(20, MinimumLength = 6,
            ErrorMessage = "The password must be at least {2} characters long.")]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [RequiredIf("Id", 0)]
        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        [System.ComponentModel.DataAnnotations.Compare("Password",
            ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Is Enabled?")]
        public bool IsEnabled { get; set; }

        public bool IsNewUser()
        {
            return Id == 0;
        }

        public IList<RoleSummary> Roles { get; set; }

        public Int32[] SelectedRoleIds { get; set; }

        public class RoleSummary
        {
            public int Id { get; set; }

            public string Name { get; set; }

            public string Description { get; set; }
        }

        public SystemUserInput()
        {
            IsEnabled = true;
            Roles = new Collection<RoleSummary>();
        }
    }
}
