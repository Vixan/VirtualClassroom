using System;
using VirtualClassroom.Core.Shared;
using VirtualClassroom.Domain;

namespace VirtualClassroom.Core
{
    class ProfessorServices : IProfessorLogic
    {
        public bool CreateActivity(int professorIdentifier, Activity activity)
        {
            // get professor from DB
            Professor professor;

            //professor.

            throw new NotImplementedException();
        }

        public bool EditActivity(int professorIdentifier, Activity activity)
        {
            throw new NotImplementedException();
        }

        public bool DeleteActivity(int professorIdentifier, int activityIdentifier)
        {
            throw new NotImplementedException();
        }

        public Activity GetActivity(int professorIdentifier, int activityIdentifier)
        {
            throw new NotImplementedException();
        }
    }
}
