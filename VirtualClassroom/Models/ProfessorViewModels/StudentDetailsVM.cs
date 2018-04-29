using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VirtualClassroom.Models.ProfessorViewModels
{
    public class StudentDetailsVM
    {
        [ForeignKey("Student")]
        public int Id { get; set; }

        [DataType(DataType.Text)]
        [StringLength(100, MinimumLength = 2)]
        public string FirstName { get; set; }

        [DataType(DataType.Text)]
        [StringLength(100, MinimumLength = 2)]
        public string LastName { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}
