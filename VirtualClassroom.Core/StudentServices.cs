using System;
using System.Collections.Generic;
using VirtualClassroom.Core.Shared;
using VirtualClassroom.Domain;

namespace VirtualClassroom.Core
{
    public class StudentServices : IStudentLogic
    {
        // private IPersistenceContext persistanceContext

        public IEnumerable<Activity> ViewActivities(Student student)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<bool> ViewActivityAttendance(int studentIdentifier, int activityIdentifier)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<int> ViewActivityGrades(int studentIdentifier, int activityIdentifier)
        {
            throw new NotImplementedException();
        }
    }
}
