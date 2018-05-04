using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using VirtualClassroom.Domain;

namespace VirtualClassroom.Persistence.EF
{
    class StudentRepository : Repository<Student>, IStudentRepository
    {
        public StudentRepository(DummyDbContext context)
        {
            dataContext = context;
        }

        public List<Activity> GetActivities(int studentIdentifier)
        {
            Student student = dataContext.Students
                .Where(s => s.Id == studentIdentifier)
                    .Include(s => s.ActivitiesLink)
                        .ThenInclude(activityLink => activityLink.Activity)
                .FirstOrDefault();

            return student.ActivitiesLink.Select(act => act.Activity).ToList();
        }

        public override IEnumerable<Student> GetAll()
        {
            List<Student> students = dataContext.Students
                .Include(student => student.ActivitiesLink)
                .Include(student => student.ActivityInfos)
                .ToList();

            return students;
        }

        public override Student GetById(int identifier)
        {
            return dataContext.Students
                .Where(student => student.Id == identifier)
                    .Include(student => student.ActivitiesLink)
                    .Include(student => student.ActivityInfos)
                        .ThenInclude(activityInfo => activityInfo.OccurenceDate)
                .FirstOrDefault();
        }

        public Student GetByEmail(string email)
        {
            return dataContext.Students
                .Where(student => student.Email == email)
                    .Include(student => student.ActivitiesLink)
                    .Include(student => student.ActivityInfos)
                .FirstOrDefault();
        }

        public Student GetByName(string name)
        {
            return dataContext.Students
                .Where(student => student.FirstName == name)
                    .Include(student => student.ActivitiesLink)
                    .Include(student => student.ActivityInfos)
                .FirstOrDefault();
        }
    }
}