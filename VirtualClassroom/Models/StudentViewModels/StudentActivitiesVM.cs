using System.Collections.Generic;
using VirtualClassroom.Domain;

namespace VirtualClassroom.Models.StudentViewModels
{
    public class StudentActivitiesVM
    {
        public IEnumerable<Activity> Activities { get; set; }

        public ICollection<Professor> Professors { get; set; }
    }
}
