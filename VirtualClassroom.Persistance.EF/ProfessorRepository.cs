using System.Linq;
using System.Collections.Generic;
using VirtualClassroom.Domain;

namespace VirtualClassroom.Persistence.EF
{
    class ProfessorRepository : Repository<Professor>, IProfessorRepository
    {
        private DummyDbContext dataContext = null;

        public ProfessorRepository(DummyDbContext context)
        {
            dataContext = context;
        }

        public List<Activity> GetActivities(int professorIdentifier)
        {
            Professor professor = dataContext.Professors.Find(professorIdentifier);

            return (List<Activity>)professor.Activities;
        }

        public Professor GetByEmail(string email)
        {
            return dataContext.Professors
                .Where(professor => professor.Email == email)
                .FirstOrDefault();
        }

        public Professor GetByName(string name)
        {
            return dataContext.Professors
                .Where(professor => professor.FirstName == name)
                .FirstOrDefault();

        }
    }
}
