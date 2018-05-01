using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VirtualClassroom.Domain;

namespace VirtualClassroom.Persistence.EF
{
   public class PersistanceContext : IPersistanceContext
    {
        private DummyDbContext dataContext = null;

        private ActivitiesRepository activitiesRepository = null;
        private ProfessorRepository professorRepository = null;
        private StudentRepository studentRepository = null;

        public PersistanceContext (IServiceProvider serviceProvider)
        {
            InitializeDbContext(serviceProvider);
        }

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

        private void InitializeDbData()
        {
            if(activitiesRepository.GetActivityType("Course") == null)
            {
                dataContext.ActitivityTypes.Add(new ActivityType { Name = "Course" });
            }

            if(activitiesRepository.GetActivityType("Laboratory") == null)
            {
                dataContext.ActitivityTypes.Add(new ActivityType { Name = "Laboratory" });
            }

            if(activitiesRepository.GetActivityType("Seminary") == null)
            {
                dataContext.ActitivityTypes.Add(new ActivityType { Name = "Seminary" });
            }

            dataContext.SaveChanges();
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
            // Initialize data below

            InitializeDbData();
        }
    }
}
