using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VirtualClassroom.Domain;
using System.Collections.Generic;

namespace VirtualClassroom.Persistence.EF
{
    public class PersistanceContext : IPersistanceContext
    {
        private DummyDbContext dataContext = null;

        private ActivitiesRepository activitiesRepository = null;
        private ProfessorRepository professorRepository = null;
        private StudentRepository studentRepository = null;

        public PersistanceContext(IServiceProvider serviceProvider)
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
            List<ActivityType> activityTypes = new List<ActivityType>
            {
                new ActivityType
                {
                    Name = "Course"
                },
                new ActivityType
                {
                    Name = "Laboratory"
                },
                new ActivityType
                {
                    Name = "Seminary"
                }
            };
            List<ActivityOccurence> activityOccurences = new List<ActivityOccurence>
            {
                new ActivityOccurence
                {
                    OccurenceDate = DateTime.Now
                },
                new ActivityOccurence
                {
                    OccurenceDate = DateTime.Now.AddDays(-1).AddHours(-5).AddMinutes(-1)
                }
            };
            List<Activity> activities = new List<Activity> {
                new Activity
                {
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
                    FirstName = "Mark",
                    LastName = "Zuckerberg",
                    Email = "mark_zucker@gmail.com",
                    //Activities = new List<Activity> { activities[0] },
                    ActivitiesLink = new List<StudentActivity>
                    {
                        new StudentActivity
                        {
                            Activity = activities[0]
                        }
                    },
                    ActivityInfos = new List<ActivityInfo>
                    {
                        new ActivityInfo
                        {
                            Grade = 6,
                            Presence = true,
                            Activity = activities[0],
                            OccurenceDate = activityOccurences[0]
                        }
                    }
                },
                new Student
                    {
                    FirstName = "Bill",
                    LastName = "Gates",
                    Email = "billgates@outlook.com",
                    //Activities = new List<Activity> { activities[1] },
                    ActivitiesLink = new List<StudentActivity>
                    {
                        new StudentActivity
                        {
                            Activity = activities[1]
                        }
                    },
                    ActivityInfos = new List<ActivityInfo>
                    {
                        new ActivityInfo
                        {
                            Grade = 0,
                            Presence = false,
                            Activity = activities[1],
                            OccurenceDate = activityOccurences[1]
                        }
                    }
                },
                new Student
                {
                    FirstName = "Dennis",
                    LastName = "Richie",
                    Email = "richiedev@mail.com",
                    //Activities = new List<Activity> { activities[0] },
                    ActivitiesLink = new List<StudentActivity>
                    {
                        new StudentActivity
                        {
                            Activity = activities[0]
                        }
                    },
                    ActivityInfos = new List<ActivityInfo>
                    {
                        new ActivityInfo
                        {
                            Grade = 10,
                            Presence = true,
                            Activity = activities[0],
                            OccurenceDate = activityOccurences[0]
                        }
                    }
                },
                new Student
                {
                    FirstName = "Linus",
                    LastName = "Torvalds",
                    Email = "linustorvalds@gmail.com",
                    //Activities = new List<Activity> { activities[0], activities[1] },
                    ActivitiesLink = new List<StudentActivity>
                    {
                        new StudentActivity
                        {
                            Activity = activities[0]
                        },
                        new StudentActivity
                        {
                            Activity = activities[1]
                        }
                    },
                    ActivityInfos = new List<ActivityInfo>
                    {
                        new ActivityInfo
                        {
                            Grade = 10,
                            Presence = true,
                            Activity = activities[0],
                            OccurenceDate = activityOccurences[0]
                        },
                        new ActivityInfo
                        {
                            Grade = 7,
                            Presence = true,
                            Activity = activities[1],
                            OccurenceDate = activityOccurences[1]
                        },
                        new ActivityInfo
                        {
                            Grade = 8,
                            Presence = true,
                            Activity = activities[0],
                            OccurenceDate = activityOccurences[1]
                        }
                    }
                }
            };
            List<Professor> professors = new List<Professor>
            {
                new Professor
                {
                    FirstName = "John",
                    LastName = "Doe",
                    Email = "johndoe@gmail.com",
                    Activities = activities
                }
            };

            activityTypes.ForEach(act =>
            {
                if (activitiesRepository.GetActivityType(act.Name) == null)
                {
                    dataContext.ActitivityTypes.Add(act);
                }
            });
            professors.ForEach(prof => dataContext.Professors.Add(prof));
            students.ForEach(stud => dataContext.Students.Add(stud));

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
            //InitializeDbData();
        }
    }
}
