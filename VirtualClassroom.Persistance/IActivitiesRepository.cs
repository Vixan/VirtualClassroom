using System.Collections.Generic;
using VirtualClassroom.Domain;

namespace VirtualClassroom.Persistence
{
    public interface IActivitiesRepository : IRepository<Activity>
    {
        Activity GetByName(string name);
        List<Activity> GetByType(ActivityType type); 
    }
}
