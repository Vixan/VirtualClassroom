using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using VirtualClassroom.Domain;

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
            List<ActivityType> activityTypes = new List<ActivityType>
            {
                new ActivityType
                {
                    Id = 1,
                    Name = "Course"
                },
                new ActivityType
                {
                    Id = 2,
                    Name = "Laboratory"
                }
            };

            List<Activity> activities = new List<Activity> {
                new Activity
                {
                    Id = 1,
                    Name = "OOP",
                    ActivityType = activityTypes[0],
                    Description = "Lorem ipsum dolor sit amet, duo ei volutpat voluptaria, his ne vero suscipiantur. Mel sapientem interesset complectitur in",
                    OccurenceDates = new List<ActivityOccurence>
                    {
                        new ActivityOccurence
                        {
                            Id = 1,
                            OccurenceDate = DateTime.Now
                        }
                    }
                },
                new Activity
                {
                    Id = 2,
                    Name = "AI",
                    ActivityType = activityTypes[1],
                    Description = "His ne vero suscipiantur. Mel sapientem interesset complectitur in",
                    OccurenceDates = new List<ActivityOccurence>
                    {
                        new ActivityOccurence
                        {
                            Id = 2,
                            OccurenceDate = DateTime.Now.AddDays(-1).AddHours(-5).AddMinutes(-1)
                        }
                    }
                }
            };

            activitiesRepository.Add(activities[0]);
            activitiesRepository.Add(activities[1]);

            professorsRepository.Add(new Professor
            {
                Id = 1,
                FirstName = "John",
                LastName = "Doe",
                Email = "johndoe@gmail.com",
                Activities = activities
            });

            studentsRepository.Add(new Student
            {
                Id = 1,
                FirstName = "Mark",
                LastName = "Zuckerberg",
                Email = "mark_zucker@gmail.com",
                Activities = new List<Activity> { activities[0] }
            });

            studentsRepository.Add(new Student
            {
                Id = 2,
                FirstName = "Bill",
                LastName = "Gates",
                Email = "billgates@outlook.com",
                Activities = new List<Activity> { activities[1] }
            });
        }
    }
}
