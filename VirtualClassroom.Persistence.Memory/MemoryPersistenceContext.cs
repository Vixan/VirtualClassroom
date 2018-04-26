using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace VirtualClassroom.Persistence.Memory
{
    public class MemoryPersistenceContext : IPersistanceContext
    {
        private MemActivitiesRepository activitiesRepository = new MemActivitiesRepository();
        private MemProfessorsRepository professorsRepository = new MemProfessorsRepository();
        private MemStudentsRepository studentsRepository = new MemStudentsRepository();

        public IActivitiesRepository GetActivitiesRepository()
        {
            return activitiesRepository;
        }

        public IProfessorRepository GetProfessorRepository()
        {
            return professorsRepository;
        }

        public IStudentRepository GetStudentRepository()
        {
            return studentsRepository;
        }

        public void InitializeContext(IServiceCollection services, IConfiguration configuration)
        {

        }

        public void InitializeData(IServiceProvider serviceProvider)
        {

        }
    }
}
