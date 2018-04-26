using System.Collections.Generic;
using VirtualClassroom.Domain;

namespace VirtualClassroom.Core.Shared
{
    public interface IProfessorServices
    {
        Professor GetProfessor(int professorId);
        void AddProfessor(Professor professor);
        void DeleteProfessor(Professor professor);
        bool CreateActivity(int professorId, Activity activity);
        bool EditActivity(int professorId, Activity activity);
        bool DeleteActivity(int professorId, int activityIdentifier);
        Activity GetActivity(int professorIdentifier, int activityIdentifier);
        IEnumerable<Activity> GetActivities(int professorIdentifier);
    }
}
