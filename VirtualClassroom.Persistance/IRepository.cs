using System.Collections.Generic;

namespace VirtualClassroom.Persistence
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T GetById(int identifier);
        void Add(T entity);
        void Delete(T entity);
        void Save();
    }
}
