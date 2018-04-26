using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        public ICollection<DateTime> OccurenceDates { get; set; }

        [ForeignKey("ActivityType")]
        public int ActivityTypeId { get; set; }
    }
}
