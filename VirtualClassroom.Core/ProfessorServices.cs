
using System.Collections.Generic;
using System.Linq;
using VirtualClassroom.Core.Shared;
using VirtualClassroom.Domain;
using VirtualClassroom.Persistence;

namespace VirtualClassroom.Core
{
    public class ProfessorServices : IProfessorServices
    {
        private readonly IPersistanceContext persistanceContext;

        public ProfessorServices(IPersistanceContext persistanceContext)
        {
            this.persistanceContext = persistanceContext;
        }

        public Professor GetProfessor(int professorIdentifier)
        {
            IProfessorRepository professorRepository = persistanceContext.GetProfessorRepository();

            return professorRepository.GetById(professorIdentifier);
        }

        public IEnumerable<Professor> GetAllProfessors()
        {
            IProfessorRepository professorRepository = persistanceContext.GetProfessorRepository();

            return professorRepository.GetAll();
        }

        public void AddProfessor(Professor professor)
        {
            IProfessorRepository professorRepository = persistanceContext.GetProfessorRepository();
            professorRepository.Add(professor);
        }

        public void DeleteProfessor(Professor professor)
        {
            IProfessorRepository professorRepository = persistanceContext.GetProfessorRepository();
            professorRepository.Delete(professor);
        }

        public bool CreateActivity(int professorIdentifier, Activity activity)
        {
            IProfessorRepository professorRepository = persistanceContext.GetProfessorRepository();
            Professor professor = professorRepository.GetById(professorIdentifier);

            professor.Activities.Add(activity);
            professorRepository.Save();

            return true;
        }

        public bool EditActivity(int professorIdentifier, Activity activity)
        {
            IProfessorRepository professorRepository = persistanceContext.GetProfessorRepository();
            Professor professor = professorRepository.GetById(professorIdentifier);

            var activityToEdit = professor.Activities.ToList().Where(act => act.Id == activity.Id).FirstOrDefault();

            if (activityToEdit == null)
            {
                return false;
            }

            activityToEdit.Name = activity.Name;
            activityToEdit.Description = activity.Description;
            activityToEdit.ActivityType = activity.ActivityType;
            activityToEdit.OccurenceDates = activity.OccurenceDates;
            activityToEdit.StudentsLink = activity.StudentsLink;

            professorRepository.Save();

            return true;
        }

        public bool DeleteActivity(int professorIdentifier, int activityIdentifier)
        {
            IProfessorRepository professorRepository = persistanceContext.GetProfessorRepository();
            Professor professor = professorRepository.GetById(professorIdentifier);

            Activity activityToDelete = null;
            foreach (var professorActivity in professor.Activities)
            {
                if (professorActivity.Id == activityIdentifier)
                {
                    activityToDelete = professorActivity;
                    break;
                }
            }

            if (activityToDelete == null)
                return false;

            professor.Activities.Remove(activityToDelete);
            professorRepository.Save();

            return true;
        }

        public Activity GetActivity(int professorIdentifier, int activityIdentifier)
        {
            IProfessorRepository professorRepository = persistanceContext.GetProfessorRepository();
            Professor professor = professorRepository.GetById(professorIdentifier);

            foreach (var professorActivity in professor.Activities)
            {
                if (professorActivity.Id == activityIdentifier)
                    return professorActivity;
            }

            return null;
        }

        public IEnumerable<Activity> GetActivities(int professorIdentifier)
        {
            IProfessorRepository professorRepository = persistanceContext.GetProfessorRepository();
            Professor professor = professorRepository.GetById(professorIdentifier);

            return professor.Activities;
        }
    }
}
