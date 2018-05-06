using System;
using System.Collections.Generic;
using System.Linq;
using VirtualClassroom.Core.Shared;
using VirtualClassroom.Domain;
using VirtualClassroom.Persistence;

namespace VirtualClassroom.Core
{
    public class ActivityServices : IActivityServices
    {
        private readonly IPersistanceContext persistanceContext;

        public ActivityServices(IPersistanceContext persistanceContext)
        {
            this.persistanceContext = persistanceContext;
        }

        public Activity GetActivity(int activityId)
        {
            IActivitiesRepository activitiesRepository = this.persistanceContext.GetActivitiesRepository();

            return activitiesRepository.GetById(activityId);
        }

        public void AddActivity(Activity activity)
        {
            IActivitiesRepository activitiesRepository = this.persistanceContext.GetActivitiesRepository();
            activitiesRepository.Add(activity);

        }

        public void DeleteActivity(Activity activity)
        {
            IActivitiesRepository activitiesRepository = this.persistanceContext.GetActivitiesRepository();
            activitiesRepository.Delete(activity);
        }

        public void EditActivity(Activity activity)
        {
            IActivitiesRepository activitiesRepository = this.persistanceContext.GetActivitiesRepository();
            List<Activity> activities = new List<Activity>(activitiesRepository.GetAll());

            var activityToEdit = activities.Where(act => act.Id == activity.Id).FirstOrDefault();

            if (activityToEdit != null)
            {
                activityToEdit.Name = activity.Name;
                activityToEdit.Description = activity.Description;
                activityToEdit.ActivityType = activity.ActivityType;
                activityToEdit.OccurenceDates = activity.OccurenceDates;

                activitiesRepository.Save();
            }
        }

        public IEnumerable<Activity> GetAllActivities()
        {
            IActivitiesRepository activitiesRepository = this.persistanceContext.GetActivitiesRepository();
            return activitiesRepository.GetAll();
        }

        public ActivityType GetActivityType(int activityId)
        {
            IActivitiesRepository activitiesRepository = this.persistanceContext.GetActivitiesRepository();
            return activitiesRepository.GetById(activityId).ActivityType;
        }

        public IEnumerable<ActivityType> GetAllActivityTypes()
        {
            IActivitiesRepository activitiesRepository = this.persistanceContext.GetActivitiesRepository();
            return activitiesRepository.GetActivityTypes();
        }

        public IEnumerable<ActivityOccurence> GetActivityOccurences(int activityIdentifier)
        {
            IActivitiesRepository activitiesRepository = this.persistanceContext.GetActivitiesRepository();
            Activity activity = activitiesRepository.GetById(activityIdentifier);

            return activity.OccurenceDates;
        }

        public IEnumerable<Student> GetStudents(Activity activity)
        {
            IActivitiesRepository activitiesRepository = this.persistanceContext.GetActivitiesRepository();
            Activity storedActivity = activity;
            if (activitiesRepository != null)
            {
                var tempActivity = activitiesRepository.GetById(activity.Id);
                if (tempActivity != null)
                {
                    storedActivity = tempActivity;
                }
            }
            IEnumerable<Student> students = storedActivity.StudentsLink.Select(studLink => studLink.Student);

            return students;
        }

        public void RemoveActivityInfo(ActivityInfo activityInfo)
        {
            IActivitiesRepository activitiesRepository = this.persistanceContext.GetActivitiesRepository();
            activitiesRepository.RemoveActivityInfo(activityInfo);

            activitiesRepository.Save();
        }

        public void RemoveStudentActivity(StudentActivity studentActivity)
        {
            IActivitiesRepository activitiesRepository = this.persistanceContext.GetActivitiesRepository();
            activitiesRepository.RemoveStudentActivity(studentActivity);

            activitiesRepository.Save();
        }
    }
}
