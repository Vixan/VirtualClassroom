using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VirtualClassroom.Domain;

namespace VirtualClassroom.Models.ProfessorViewModels
{
    public class ActivityDetailsVM
    {
        [ForeignKey("Activity")]
        [Required(ErrorMessage = "Id required")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name required")]
        [DataType(DataType.Text)]
        [StringLength(100, MinimumLength = 2)]
        public string Name { get; set; }
        
        public ICollection<ActivityStudentVM> Students { get; set; }

        public ICollection<ActivityOccurence> OccurenceDates { get; set; }
    }
}
