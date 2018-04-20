using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace VirtualClassroom.Domain
{
    public class Activity
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Activity Name is required")]
        [DataType(DataType.Text)]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Name must be 2 to 50 characters long")]
        public int Name { get; set; }

        [DataType(DataType.MultilineText)]
        [MaxLength(255, ErrorMessage = "Name must be maximum 255 characters long")]
        public int Description { get; set; }

        public virtual ICollection<ActivityOccurence> OccurenceDates { get; set; }

        [Required(ErrorMessage = "Activity Type is required")]
        public ActivityType ActivityType { get; set; }
    }
}
