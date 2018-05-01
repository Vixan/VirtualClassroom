using System.Collections.Generic;

namespace VirtualClassroom.Domain
{
    public class Professor
    {
        public int Id { get; set; }
        
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        public ICollection<Activity> Activities { get; set; }
    }
}
