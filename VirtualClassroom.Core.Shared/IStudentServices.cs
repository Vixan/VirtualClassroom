using System.Collections.Generic;
using VirtualClassroom.Domain;

namespace VirtualClassroom.Core.Shared
{
    public interface IStudentServices
    {
        Student GetStudent(int studentId);
        IEnumerable<Student> GetAllStudents();
        void AddStudent(Student student);
        void DeleteStudent(Student student);
        IEnumerable<Activity> GetActivities(int studentIdentifier);
        Activity GetActivity(int studentIdentifier, int activityIdentifier);
        ActivityInfo GetActivityInfo(int studentIdentifier, int activityIdentifier);
        bool EditActivity(int studentIdentifier, ActivityInfo activityInfo);
        IEnumerable<int> GetActivityGrades(int studentIdentifier, int activityIdentifier);
        IEnumerable<bool> GetActivityAttendance(int studentIdentifier, int activityIdentifier);
    }
}
