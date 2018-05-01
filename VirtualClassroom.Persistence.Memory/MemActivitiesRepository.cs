using System;
using System.Collections.Generic;
using System.Text;
using VirtualClassroom.Domain;

namespace VirtualClassroom.Persistence.Memory
{
    public class MemActivitiesRepository : IActivitiesRepository
    {
        List<Activity> activities = new List<Activity>();

        public void Add(Activity activity)
        {
            activities.Add(activity);
        }

        public void Delete(Activity activity)
        {
            activities.Remove(activity);
        }

        public ActivityType GetActivityType(string activityTypeName)
        {
            throw new NotImplementedException();
        }

        public List<ActivityType> GetActivityTypes()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Activity> GetAll()
        {
            return activities;
        }

        public Activity GetById(int identifier)
        {
            return activities.Find(activity => activity.Id == identifier);
        }

        public Activity GetByName(string name)
        {
            return activities.Find(activity => activity.Name == name);
        }

        public List<Activity> GetByType(ActivityType type)
        {
            return activities.FindAll(activity => activity.ActivityType == type);
        }

        public void Save()
        {

        }
    }
}
