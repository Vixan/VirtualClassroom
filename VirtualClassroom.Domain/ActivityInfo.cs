using System.ComponentModel.DataAnnotations;

namespace VirtualClassroom.Domain
{
    public class ActivityInfo
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Activity Identifier is required")]
        public int ActivityId { get; set; }

        [Required(ErrorMessage = "Student Identifier is required")]
        public int Student { get; set; }

        [Range(1, 10, ErrorMessage = "Grade can only be between 1 and 10")]
        public int Grade { get; set; }

        [Required(ErrorMessage = "Student Presence is required")]
        public bool Presence { get; set; }

        [Required(ErrorMessage = "Occurence Date is required")]
        public ActivityOccurence OccurenceDate { get; set; }
    }
}
