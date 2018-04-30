using System;
using System.Collections.Generic;
using System.Text;
using VirtualClassroom.Domain;

namespace VirtualClassroom.Persistence.Memory
{
    public class MemProfessorsRepository : IProfessorRepository
    {
        List<Professor> professors = new List<Professor>();

        public void Add(Professor professor)
        {
            professors.Add(professor);
        }

        public void Delete(Professor professor)
        {
            professors.Remove(professor);
        }

        public List<Activity> GetActivities(int professorIdentifier)
        {
            var professor = professors.Find(prof => prof.Id == professorIdentifier);

            return new List<Activity>(professor.Activities);
        }

        public IEnumerable<Professor> GetAll()
        {
            return professors;
        }

        public Professor GetByEmail(string email)
        { 
            return professors.Find(prof => prof.Email == email);
        }

        public Professor GetById(int identifier)
        {
            return professors.Find(prof => prof.Id == identifier);
        }

        public Professor GetByName(string name)
        {
            return professors.Find(prof =>
                $"{prof.FirstName} {prof.LastName}" == name ||
                $"{prof.LastName} {prof.FirstName}" == name
            );
        }

        public void Save()
        {
            
        }
    }
}
