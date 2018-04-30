using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VirtualClassroom.Models.ProfessorViewModels
{
    public class ActivityStudentInfoVM
    {
        [ForeignKey("ActivityInfo")]
        [Required(ErrorMessage = "Id required")]
        public int Id { get; set; }

        [DataType(DataType.Text)]
        public string StudentName;

        [DataType(DataType.Text)]
        public string ActivityName;

        [Required(ErrorMessage = "Datetime required")]
        public DateTime OccurenceDate { get; set; }

        [Required(ErrorMessage = "Grade required")]
        [Range(1, 10, ErrorMessage = "Grade must be between 1 and 10")]
        public int Grade { get; set; }

        [Required(ErrorMessage = "Presence required")]
        public bool Presence { get; set; }
    }
}
