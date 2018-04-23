using System.Collections.Generic;
using VirtualClassroom.Domain;

namespace VirtualClassroom.Persistence
{
    public interface IProfessorRepository : IRepository<Professor>
    {
        Professor GetByName(string name);
        Professor GetByEmail(string email);
        List<Activity> GetActivities();
        List<ActivityInfo> GetActivityInfos();
    }
}
