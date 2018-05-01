namespace VirtualClassroom.Domain
{
    public class ActivityInfo
    {
        public int Id { get; set; }
        
        public int ActivityId { get; set; }
        public int Student { get; set; }
        
        public int Grade { get; set; }
        public bool Presence { get; set; }
        public ActivityOccurence OccurenceDate { get; set; }
    }
}
