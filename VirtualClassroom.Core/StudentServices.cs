using System.Collections.Generic;
using VirtualClassroom.Core.Shared;
using VirtualClassroom.Domain;
using VirtualClassroom.Persistence;

namespace VirtualClassroom.Core
{
    public class StudentServices : IStudentServices
    {
        private readonly IPersistanceContext persistanceContext;

        public StudentServices(IPersistanceContext persistanceContext)
        {
            this.persistanceContext = persistanceContext;
        }

        public Student GetStudent(int studentIdentifier)
        {
            IStudentRepository studentRepository = persistanceContext.GetStudentRepository();

            return studentRepository.GetById(studentIdentifier);
        }

        public IEnumerable<Student> GetAllStudents()
        {
            IStudentRepository studentRepository = persistanceContext.GetStudentRepository();

            return studentRepository.GetAll();
        }

        public void AddStudent(Student student)
        {
            IStudentRepository studentRepository = persistanceContext.GetStudentRepository();

            studentRepository.Add(student);
        }

        public void DeleteStudent(Student student)
        {
            IStudentRepository studentRepository = persistanceContext.GetStudentRepository();

            studentRepository.Delete(student);
        }

        public IEnumerable<Activity> GetActivities(int studentIdentifier)
        {
            IStudentRepository studentRepository = persistanceContext.GetStudentRepository();
            Student student = studentRepository.GetById(studentIdentifier);

            return student.Activities;
        }

        public IEnumerable<bool> GetActivityAttendance(int studentIdentifier, int activityIdentifier)
        {
            IStudentRepository studentRepository = persistanceContext.GetStudentRepository();
            Student student = studentRepository.GetById(studentIdentifier);

            List<bool> activityAttendance = new List<bool>();
            foreach(var studentActivityInfo in student.ActivityInfos)
            {
                if (studentActivityInfo.Presence == true && studentActivityInfo.ActivityId == activityIdentifier)
                    activityAttendance.Add(true);
            }

            return activityAttendance;
        }

        public IEnumerable<int> GetActivityGrades(int studentIdentifier, int activityIdentifier)
        {
            IStudentRepository studentRepository = persistanceContext.GetStudentRepository();
            Student student = studentRepository.GetById(studentIdentifier);

            List<int> activityGrades = new List<int>();
            foreach (var studentActivityInfo in student.ActivityInfos)
            {
                if (studentActivityInfo.ActivityId == activityIdentifier)
                    activityGrades.Add(studentActivityInfo.Grade);
            }

            return activityGrades;
        }
    }
}
