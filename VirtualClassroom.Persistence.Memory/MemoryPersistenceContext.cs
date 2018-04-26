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
            List<Activity> activities = new List<Activity> {
                new Activity
                {
                    Id = 1,
                    Name = "OOP",
                    ActivityType = new ActivityType
                    {
                        Id = 1,
                        Name = "Course"
                    },
                    Description = "Lorem ipsum dolor sit amet, duo ei volutpat voluptaria, his ne vero suscipiantur. Mel sapientem interesset complectitur in",
                    OccurenceDates = new List<ActivityOccurence>
                    {
                        new ActivityOccurence
                        {
                            Id = 1,
                            OccurenceDate = new DateTime()
                        }
                    }
                },
                new Activity
                {
                    Id = 2,
                    Name = "AI",
                    ActivityType = new ActivityType
                    {
                        Id = 2,
                        Name = "Laboratory"
                    },
                    Description = "His ne vero suscipiantur. Mel sapientem interesset complectitur in",
                    OccurenceDates = new List<ActivityOccurence>
                    {
                        new ActivityOccurence
                        {
                            Id = 2,
                            OccurenceDate = new DateTime()
                        }
                    }
                }
            };

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
