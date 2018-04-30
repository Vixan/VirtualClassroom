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
                .FirstOrDefault();

            return (List<Activity>)professor.Activities;
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
            return dataContext.Professors
                .Where(professor => professor.Id == identifier)
                    .Include(professor => professor.Activities)
                .FirstOrDefault();
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
