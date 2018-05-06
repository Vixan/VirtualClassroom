using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VirtualClassroom.Domain;

namespace VirtualClassroom.Models.ProfessorViewModels
{
    public class ActivityEditVM
    {
        [ForeignKey("Activity")]
        [Required(ErrorMessage = "Id required")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name required")]
        [DataType(DataType.Text)]
        [StringLength(100, MinimumLength = 2)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description required")]
        [DataType(DataType.Text)]
        [StringLength(100, MinimumLength = 2)]
        public string Description { get; set; }

        [Display(Name = "Occurence")]
        public ICollection<DateTime> OccurenceDates { get; set; }

        [ForeignKey("ActivityType")]
        [Display(Name="Activity Type")]
        public int ActivityTypeId { get; set; }

        public ICollection<ActivityType> ActivityTypes { get; set; }

        [Display(Name = "Students")]
        public ICollection<int> SelectedStudentsId { get; set; }

        public ICollection<int> OtherStudentsId { get; set; }
    }
}
