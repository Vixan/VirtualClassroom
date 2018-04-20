using System.Collections.Generic;
using VirtualClassroom.Domain;

namespace VirtualClassroom.Core.Shared
{
    public interface IStudentLogic
    {
        IEnumerable<Activity> ViewActivities(Student student);

        IEnumerable<int> ViewActivityGrades(int studentIdentifier, int activityIdentifier);

        IEnumerable<bool> ViewActivityAttendance(int studentIdentifier, int activityIdentifier);
    }
}
