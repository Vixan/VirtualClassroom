using System.Collections.Generic;
using VirtualClassroom.Domain;

namespace VirtualClassroom.Persistence
{
    public interface IStudentRepository : IRepository<Student>
    {
        Student GetByName(string name);
        Student GetByEmail(string email);
        List<Activity> GetActivities();
    }
}
