using System;
using VirtualClassroom.Core.Shared;
using VirtualClassroom.Domain;

namespace VirtualClassroom.Core
{
    class ProfessorServices : IProfessorLogic
    {
        // private IPersistanceContext persistanceContext;

        public bool CreateActivity(int professorIdentifier, Activity activity)
        {
            
            Professor professor = new Professor();

            professor.Activities.Add(activity);

            // save changes

            return true;
        }

        public bool EditActivity(int professorIdentifier, Activity activity)
        {
            // get professo from Db
            Professor professor = new Professor();
            Activity activity_to_edit = null;
            
            foreach(var professor_activity in professor.Activities)
            {
                if(professor_activity.Id == activity.Id)
                {
                    activity_to_edit = professor_activity;
                    break;
                }
            }

            if (activity_to_edit == null)
                return false;

            activity_to_edit = activity;

            // save changes

            return true;
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
