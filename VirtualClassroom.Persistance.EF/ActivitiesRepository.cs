using System.Linq;
using System.Collections.Generic;
using VirtualClassroom.Domain;

namespace VirtualClassroom.Persistence.EF
{
    class ActivitiesRepository : Repository<Activity>, IActivitiesRepository
    {
        private DummyDbContext dataContext = null;

        public ActivitiesRepository(DummyDbContext context)
        {
            dataContext = context;
        }

        public Activity GetByName(string name)
        {
            return dataContext.Activities
                .Where(activity => activity.Name == name)
                .FirstOrDefault();
            
        }

        public List<Activity> GetByType(ActivityType type)
        {
            List<Activity> activityList = (List<Activity>)from activity in dataContext.Activities
                                                          where activity.ActivityType.Equals(type)
                                                          select activity;

            return activityList;
        }
    }
}
