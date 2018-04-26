using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VirtualClassroom.Models.ProfessorViewModels
{
    public class ActivityStudentVM
    {
        [ForeignKey("Student")]
        [Required(ErrorMessage = "Id required")]
        public int Id { get; set; }

        [Required(ErrorMessage = "First name required")]
        [DataType(DataType.Text)]
        [StringLength(100, MinimumLength = 2)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name required")]
        [DataType(DataType.Text)]
        [StringLength(100, MinimumLength = 2)]
        public string LastName { get; set; }

        public ActivityInfoVM ActivityInfo { get; set; }
    }
}
