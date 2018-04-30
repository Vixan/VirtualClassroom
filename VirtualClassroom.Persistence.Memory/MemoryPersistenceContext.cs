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
            List<ActivityOccurence> activityOccurences = new List<ActivityOccurence>
            {
                new ActivityOccurence
                {
                    Id = 1,
                    OccurenceDate = DateTime.Now
                },
                 new ActivityOccurence
                {
                    Id = 2,
                    OccurenceDate = DateTime.Now.AddDays(-1).AddHours(-5).AddMinutes(-1)
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
                        activityOccurences[0],
                        activityOccurences[1]
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
                        activityOccurences[1]
                    }
                }
            };
            List<Student> students = new List<Student>
            {
                new Student
                {
                    Id = 1,
                    FirstName = "Mark",
                    LastName = "Zuckerberg",
                    Email = "mark_zucker@gmail.com",
                    Activities = new List<Activity> { activities[0] },
                    ActivityInfos = new List<ActivityInfo>
                    {
                        new ActivityInfo
                        {
                            Id = 1,
                            Grade = 6,
                            Presence = true,
                            ActivityId = activities[0].Id,
                            OccurenceDate = activityOccurences[0]
                        }
                    }
                },
                new Student
                    {
                    Id = 2,
                    FirstName = "Bill",
                    LastName = "Gates",
                    Email = "billgates@outlook.com",
                    Activities = new List<Activity> { activities[1] },
                    ActivityInfos = new List<ActivityInfo>
                    {
                        new ActivityInfo
                        {
                            Id = 2,
                            Grade = 0,
                            Presence = false,
                            ActivityId = activities[1].Id,
                            OccurenceDate = activityOccurences[1]
                        }
                    }
                },
                new Student
                {
                    Id = 3,
                    FirstName = "Dennis",
                    LastName = "Richie",
                    Email = "richiedev@mail.com",
                    Activities = new List<Activity> { activities[0] },
                    ActivityInfos = new List<ActivityInfo>
                    {
                        new ActivityInfo
                        {
                            Id = 3,
                            Grade = 10,
                            Presence = true,
                            ActivityId = activities[0].Id,
                            OccurenceDate = activityOccurences[0]
                        }
                    }
                },
                new Student
                {
                    Id = 4,
                    FirstName = "Linus",
                    LastName = "Torvalds",
                    Email = "linustorvalds@gmail.com",
                    Activities = new List<Activity> { activities[0], activities[1] },
                    ActivityInfos = new List<ActivityInfo>
                    {
                        new ActivityInfo
                        {
                            Id = 3,
                            Grade = 10,
                            Presence = true,
                            ActivityId = activities[0].Id,
                            OccurenceDate = activityOccurences[0]
                        },
                        new ActivityInfo
                        {
                            Id = 4,
                            Grade = 7,
                            Presence = true,
                            ActivityId = activities[1].Id,
                            OccurenceDate = activityOccurences[1]
                        },
                        new ActivityInfo
                        {
                            Id = 5,
                            Grade = 8,
                            Presence = true,
                            ActivityId = activities[0].Id,
                            OccurenceDate = activityOccurences[1]
                        }
                    }
                }
            };
            List<Professor> professors = new List<Professor>
            {
                new Professor
                {
                    Id = 1,
                    FirstName = "John",
                    LastName = "Doe",
                    Email = "johndoe@gmail.com",
                    Activities = activities
                }
            };

            professorsRepository.Add(professors[0]);

            studentsRepository.Add(students[0]);
            studentsRepository.Add(students[1]);
            studentsRepository.Add(students[2]);
            studentsRepository.Add(students[3]);
        }
    }
}
