using ElectronicRecyclers.One800Recycling.Domain.Entities;
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
    public class NoteInput : ViewModel
    {
        [HiddenInput]
        public int Id { get; set; }

        [HiddenInput]
        public int NoteBelongsToId { get; set; }

        [Required]
        [StringLength(600)]
        [Display(Name = "Text")]
        [DataType(DataType.MultilineText)]
        public string Text { get; set; }

        [Display(Name = "Access")]
        public string[] AccessLevels
        {
            get
            {
                return Enum.GetNames(typeof(AccessLevel));
            }
        }

        [Required]
        public string SelectedAccessLevel { get; set; }

        public bool IsNewNote()
        {
            return Id == 0;
        }
    }
}
