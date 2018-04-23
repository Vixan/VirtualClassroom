using System.Collections.Generic;
using System.Linq;
using VirtualClassroom.Domain;

namespace VirtualClassroom.Persistence.EF
{
    class ActivitiesRepository : Repository<Activity>, IActivitiesRepository
    {
        public Activity GetByName(string name)
        {
            using(var context = new DummyDbContext())
            {
                return context.Activities
                    .Where(activity => activity.Name == name)
                    .FirstOrDefault();
            }
        }

        public List<Activity> GetByType(ActivityType type)
        {
            using(var context = new DummyDbContext())
            {
                List<Activity> activityList = (List<Activity>)from activity in context.Activities
                                              where activity.ActivityType.Equals(type)
                                              select activity;

                return activityList;
            }
        }
    }
}
