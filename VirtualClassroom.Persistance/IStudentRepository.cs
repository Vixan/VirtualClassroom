using System;
using System.Collections.Generic;
using System.Text;
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
