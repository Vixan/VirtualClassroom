using System.Collections.Generic;

namespace VirtualClassroom.Domain
{
    public class Activity
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<ActivityOccurence> OccurenceDates { get; set; }
        
        public ActivityType ActivityType { get; set; }
    }
}
