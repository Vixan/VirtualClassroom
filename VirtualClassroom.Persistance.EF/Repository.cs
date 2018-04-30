using System.Collections.Generic;

namespace VirtualClassroom.Persistence.EF
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected DummyDbContext dataContext = null;

        public void Add(T entity)
        {
            dataContext.Set<T>().Add(entity);
            Save();
        }

        public void Delete(T entity)
        {
            dataContext.Set<T>().Remove(entity);
            Save();
        }

        public virtual IEnumerable<T> GetAll()
        {
            List<T> data = new List<T>();

            foreach (var item in dataContext.Set<T>())
                data.Add(item);

            return data;
        }

        public virtual T GetById(int identifier)
        {
            return dataContext.Set<T>().Find(identifier);
        }

        public void Save()
        {
            dataContext.SaveChanges();
        }
    }
}
