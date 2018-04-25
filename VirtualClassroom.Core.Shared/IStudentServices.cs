using System.Collections.Generic;
using VirtualClassroom.Domain;

namespace VirtualClassroom.Core.Shared
{
    public interface IStudentServices
    {
        IEnumerable<Activity> ViewActivities(int studentIdentifier);

        IEnumerable<int> ViewActivityGrades(int studentIdentifier, int activityIdentifier);

        IEnumerable<bool> ViewActivityAttendance(int studentIdentifier, int activityIdentifier);
    }
}
