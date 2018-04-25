using System.Linq;
using System.Collections.Generic;
using VirtualClassroom.Domain;

namespace VirtualClassroom.Persistence.EF
{
    class StudentRepository : Repository<Student>, IStudentRepository
    {
        private DummyDbContext dataContext = null;

        public StudentRepository(DummyDbContext context)
        {
            dataContext = context;
        }

        public List<Activity> GetActivities(int studentIdentifier)
        {
            Student student = dataContext.Students.Find(studentIdentifier);

            return (List<Activity>)student.Activities;

        }

        public Student GetByEmail(string email)
        {
            return dataContext.Students
                   .Where(student => student.Email == email)
                   .FirstOrDefault();

        }

        public Student GetByName(string name)
        {
            return dataContext.Students
                .Where(student => student.FirstName == name)
                .FirstOrDefault();

        }
    }
}