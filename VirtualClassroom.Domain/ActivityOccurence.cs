using System;

namespace VirtualClassroom.Domain
{
    public class ActivityOccurence
    {
        public int Id { get; set; }

        public DateTime OccurenceDate { get; set; }
        public virtual Activity Activity { get; set; }
    }
}
