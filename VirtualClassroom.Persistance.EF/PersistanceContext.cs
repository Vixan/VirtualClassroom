using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace VirtualClassroom.Persistence.EF
{
   public class PersistanceContext : IPersistanceContext
    {
        private DummyDbContext dataContext = null;

        private ActivitiesRepository activitiesRepository = null;
        private ProfessorRepository professorRepository = null;
        private StudentRepository studentRepository = null;

        private void InitializeDbContext(IServiceProvider serviceProvider)
        {
            if (dataContext == null)
            {
                dataContext = serviceProvider.GetService<DummyDbContext>();

                activitiesRepository = new ActivitiesRepository(dataContext);
                professorRepository = new ProfessorRepository(dataContext);
                studentRepository = new StudentRepository(dataContext);
            }
        }

        public IActivitiesRepository GetActivitiesRepository()
        {
            return activitiesRepository;
        }

        public IProfessorRepository GetProfessorRepository()
        {
            return professorRepository;
        }

        public IStudentRepository GetStudentRepository()
        {
            return studentRepository;
        }

        public void InitializeContext(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DummyDbContext>(options =>
              options.UseSqlServer(configuration.GetConnectionString("AuthConnection"),
              b => b.MigrationsAssembly("VirtualClassroom.Persistence.EF")));

            InitializeDbContext(services.BuildServiceProvider());
        }

        public void InitializeData(IServiceProvider serviceProvider)
        {
            InitializeDbContext(serviceProvider);

            // Initialize data below
        }
    }
}
