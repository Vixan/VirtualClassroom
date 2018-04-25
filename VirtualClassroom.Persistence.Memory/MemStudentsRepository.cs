using System;
using System.Collections.Generic;
using System.Text;
using VirtualClassroom.Domain;

namespace VirtualClassroom.Persistence.Memory
{
    public class MemStudentsRepository : IStudentRepository
    {
        List<Student> students = new List<Student>();

        public void Add(Student entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Student entity)
        {
            throw new NotImplementedException();
        }

        public List<Activity> GetActivities()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Student> GetAll()
        {
            throw new NotImplementedException();
        }

        public Student GetByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public Student GetById(int identifier)
        {
            throw new NotImplementedException();
        }

        public Student GetByName(string name)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }
    }
}
