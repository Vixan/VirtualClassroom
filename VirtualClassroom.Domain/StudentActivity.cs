namespace VirtualClassroom.Domain
{
    public class StudentActivity
    {
        public int Id { get; set; }
        public virtual Student Student { get; set; }
        public virtual Activity Activity { get; set; }
    }
}
