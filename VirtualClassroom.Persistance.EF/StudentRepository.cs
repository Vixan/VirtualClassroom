using System.Collections.Generic;
using System.Linq;
using VirtualClassroom.Domain;

namespace VirtualClassroom.Persistence.EF
{
    class StudentRepository : Repository<Student>, IStudentRepository
    {
        public List<Activity> GetActivities(int studentIdentifier)
        {
            using(var context = new DummyDbContext())
            {
                Student student = context.Students.Find(studentIdentifier);

                return (List<Activity>)student.Activities;
            }
        }

        public Student GetByEmail(string email)
        {
            using(var context = new DummyDbContext())
            {
                return context.Students
                       .Where(student => student.Email == email)
                       .FirstOrDefault();
            }
        }

        public Student GetByName(string name)
        {
            using(var context = new DummyDbContext())
            {
                return context.Students
                    .Where(student => student.FirstName == name)
                    .FirstOrDefault();
            }
        }
    }
}