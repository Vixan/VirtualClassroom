using System.Collections.Generic;
using System.Linq;
using VirtualClassroom.Domain;

namespace VirtualClassroom.Persistence.EF
{
    class ProfessorRepository : Repository<Professor>, IProfessorRepository
    {
        public List<Activity> GetActivities(int professorIdentifier)
        {
            using(var context = new DummyDbContext())
            {
                Professor professor = context.Professors.Find(professorIdentifier);

                return (List<Activity>)professor.Activities;
            }
        }

        public Professor GetByEmail(string email)
        {
            using (var context = new DummyDbContext())
            {
                return context.Professors
                    .Where(professor => professor.Email == email)
                    .FirstOrDefault();
            }
        }

        public Professor GetByName(string name)
        {
            using (var context = new DummyDbContext())
            {
                return context.Professors
                    .Where(professor => professor.FirstName == name)
                    .FirstOrDefault();
            }
        }
    }
}
