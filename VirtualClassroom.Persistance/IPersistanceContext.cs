using VirtualClassroom.CommonAbstractions;

namespace VirtualClassroom.Persistence
{
    public interface IPersistanceContext: IInitializer
    {
        IProfessorRepository GetProfessorRepository();
        IStudentRepository GetStudentRepository();
        IActivitiesRepository GetActivitiesRepository();
    }
}
