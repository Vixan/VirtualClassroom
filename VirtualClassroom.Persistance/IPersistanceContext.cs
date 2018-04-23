namespace VirtualClassroom.Persistence
{
    public interface IPersistanceContext
    {
        IProfessorRepository GetProfessorRepository();
        IStudentRepository GetStudentRepository();
        IActivitiesRepository GetActivitiesRepository();
    }
}
