using System;

namespace VirtualClassroom.Persistence.Memory
{
    public class MemoryPersistenceContext : IPersistanceContext
    {
        public IProfessorRepository GetActivitiesRepository()
        {
            return new MemActivitiesRepository();
        }

        public IProfessorRepository GetProfessorRepository()
        {
            return new MemProfessorsRepository();
        }

        public IStudentRepository GetStudentRepository()
        {
            return new MemStudentsRepository();
        }
    }
}
