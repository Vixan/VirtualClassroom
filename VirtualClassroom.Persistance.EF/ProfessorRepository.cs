using System.Linq;
using System.Collections.Generic;
using VirtualClassroom.Domain;
using Microsoft.EntityFrameworkCore;

namespace VirtualClassroom.Persistence.EF
{
    class ProfessorRepository : Repository<Professor>, IProfessorRepository
    {
        public ProfessorRepository(DummyDbContext context)
        {
            dataContext = context;
        }

        public List<Activity> GetActivities(int professorIdentifier)
        {
            Professor professor = dataContext.Professors
                .Where(p => p.Id == professorIdentifier)
                    .Include(p => p.Activities)
                        .ThenInclude(activity => activity.ActivityType)                    
                    .Include(p => p.Activities)
                        .ThenInclude(activity => activity.OccurenceDates)
                    .Include(p => p.Activities)
                        .ThenInclude(activity => activity.StudentsLink)
                .FirstOrDefault();

            return professor.Activities.ToList();
        }

        public override IEnumerable<Professor> GetAll()
        {
            List<Professor> professors = dataContext.Professors
                .Include(professor => professor.Activities)
                .ToList();

            return professors;
        }

        public override Professor GetById(int identifier)
        {
            var retProf = dataContext.Professors
                .Where(professor => professor.Id == identifier)                    
                .FirstOrDefault();

            if (retProf != null)
            {
                retProf.Activities = GetActivities(identifier);
            }

            return retProf;
        }

        public Professor GetByEmail(string email)
        {
            return dataContext.Professors
                .Where(professor => professor.Email == email)
                    .Include(professor => professor.Activities)
                .FirstOrDefault();
        }

        public Professor GetByName(string name)
        {
            return dataContext.Professors
                .Where(professor => professor.FirstName == name)
                    .Include(professor => professor.Activities)
                .FirstOrDefault();
        }
    }
}
