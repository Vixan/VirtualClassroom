using System.Collections.Generic;
using VirtualClassroom.Domain;

namespace VirtualClassroom.Core.Shared
{
    public interface IStudentServices
    {
        IEnumerable<Activity> GetActivities(int studentIdentifier);

        IEnumerable<int> GetActivityGrades(int studentIdentifier, int activityIdentifier);

        IEnumerable<bool> GetActivityAttendance(int studentIdentifier, int activityIdentifier);
    }
}
