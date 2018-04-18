using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace VirtualClassroom.Domain
{
    class ActivityInfo
    {
        public int Identifier { get; set; }

        [Required(ErrorMessage = "Student Identifier is required")]
        public int StudentIdentifier { get; set; }

        [Range(1, 10, ErrorMessage = "Grade can only be between 1 and 10")]
        public int Grade { get; set; }

        [Required(ErrorMessage = "Student Presence is required")]
        public bool Present { get; set; }

        public DateTime TookPlaceOn { get; set; }
    }
}
