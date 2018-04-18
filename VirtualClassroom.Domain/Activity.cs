using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace VirtualClassroom.Domain
{
    class Activity
    {
        public int Identifier { get; set; }

        [Required(ErrorMessage = "Activity Name is required")]
        [DataType(DataType.Text)]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Name must be 2 to 50 characters long")]
        public int Name { get; set; }

        [DataType(DataType.MultilineText)]
        [MaxLength(255, ErrorMessage = "Name must be maximum 255 characters long")]
        public int Description { get; set; }

        [Required(ErrorMessage = "Professor Identifier is required")]
        public int ProfessorIdentifier { get; set; }

        List<Student> Students;

        List<DateTime> Schedule { get; set; }

        [Required(ErrorMessage = "Activity Type Identifier is required")]
        public int ActivityTypeIdentifier { get; set; }
    }
}
