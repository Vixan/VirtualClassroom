using System.Collections.Generic;
using VirtualClassroom.Core.Shared;
using VirtualClassroom.Domain;
using VirtualClassroom.Persistence;

namespace VirtualClassroom.Core
{
    public class StudentServices : IStudentLogic
    {
        private readonly IPersistanceContext persistanceContext;

        public StudentServices(IPersistanceContext persistanceContext)
        {
            this.persistanceContext = persistanceContext;
        }

        public IEnumerable<Activity> ViewActivities(int studentIdentifier)
        {
            IStudentRepository studentRepository = persistanceContext.GetStudentRepository();
            Student student = studentRepository.GetById(studentIdentifier);

            return student.Activities;
        }

        public IEnumerable<bool> ViewActivityAttendance(int studentIdentifier, int activityIdentifier)
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

        public IEnumerable<int> ViewActivityGrades(int studentIdentifier, int activityIdentifier)
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
