using System.Collections.Generic;
using VirtualClassroom.Domain;

namespace VirtualClassroom.Core.Shared
{
    public interface IProfessorServices
    {
        bool CreateActivity(int professorId, Activity activity);

        bool EditActivity(int professorId, Activity activity);

        bool DeleteActivity(int professorId, int activityIdentifier);

        Activity GetActivity(int professorIdentifier, int activityIdentifier);

        IEnumerable<Activity> GetActivities(int professorIdentifier);
    }
}
