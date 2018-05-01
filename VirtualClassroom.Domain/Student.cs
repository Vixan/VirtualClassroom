using System.Collections.Generic;

namespace VirtualClassroom.Domain
{
    public class Student
    {
        public int Id { get; set; }
        
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        public ICollection<Activity> Activities { get; set; }

        public ICollection<ActivityInfo> ActivityInfos { get; set; }
    }
}
