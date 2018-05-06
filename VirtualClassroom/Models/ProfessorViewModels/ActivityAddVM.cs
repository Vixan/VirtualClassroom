using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VirtualClassroom.Models.ProfessorViewModels
{
    public class ActivityAddVM
    {
        [Required(ErrorMessage = "Name required")]
        [DataType(DataType.Text)]
        [StringLength(100, MinimumLength = 2)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description required")]
        [DataType(DataType.Text)]
        [StringLength(256)]
        public string Description { get; set; }

        [Display(Name = "Occurence")]
        public ICollection<DateTime> OccurenceDates { get; set; }

        [Required(ErrorMessage ="Activity type required")]
        [Display(Name = "Activity Type")]
        public string ActivityTypeName { get; set; }

        [Display(Name = "Students")]
        public ICollection<int> StudentsId { get; set; }
    }
}
