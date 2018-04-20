using System;
using System.Collections.Generic;
using System.Text;

namespace VirtualClassroom.Domain
{
    public class ActivityOccurence
    {
        public int Id { get; set; }
        public DateTime OccurenceDate { get; set; }
        public virtual Activity Activity { get; set; }
    }
}
