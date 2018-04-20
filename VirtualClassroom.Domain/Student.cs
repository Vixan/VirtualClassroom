using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace VirtualClassroom.Domain
{
    public class Student
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Student first name is required")]
        [DataType(DataType.Text)]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Name must be 2 to 100 characters long")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Student last name is required")]
        [DataType(DataType.Text)]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Name must be 2 to 100 characters long")]
        public string LastName { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        ICollection<Activity> Activities { get; set; }

        ICollection<ActivityInfo> ActivityInfos { get; set; }
    }
}
