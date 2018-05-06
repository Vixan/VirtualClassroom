using System;
using System.Collections.Generic;
using System.Text;
using VirtualClassroom.Domain;

namespace VirtualClassroom.Core.Shared
{
    public interface IActivityServices
    {
        Activity GetActivity(int activityId);
        IEnumerable<Activity> GetAllActivities();

        void AddActivity(Activity activity);
        void DeleteActivity(Activity activity);
        void EditActivity(Activity activity);

        ActivityType GetActivityType(int activityId);
        IEnumerable<ActivityType> GetAllActivityTypes();

        IEnumerable<ActivityOccurence> GetActivityOccurences(int activityIdentifier);
        IEnumerable<Student> GetStudents(Activity activity);

        void RemoveActivityInfo(ActivityInfo activityInfo);
        void RemoveStudentActivity(StudentActivity studentActivity);
    }
}
