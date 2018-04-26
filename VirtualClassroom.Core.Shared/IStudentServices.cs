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
        IEnumerable<int> GetActivityGrades(int studentIdentifier, int activityIdentifier);
        IEnumerable<bool> GetActivityAttendance(int studentIdentifier, int activityIdentifier);
    }
}
