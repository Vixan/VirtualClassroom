using System;
using System.Collections.Generic;
using System.Text;
using VirtualClassroom.Domain;

namespace VirtualClassroom.Persistence.Memory
{
    public class MemActivitiesRepository : IProfessorRepository
    {
        public void Add(Activity entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Activity entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Activity> GetAll()
        {
            throw new NotImplementedException();
        }

        public Activity GetById(int identifier)
        {
            throw new NotImplementedException();
        }

        public Activity GetByName(string name)
        {
            throw new NotImplementedException();
        }

        public List<Activity> GetByType(ActivityType type)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }
    }
}
