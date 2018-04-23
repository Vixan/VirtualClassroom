using System.Collections.Generic;

namespace VirtualClassroom.Persistence.EF
{
    public class Repository<T> : IRepository<T> where T : class
    {

        public void Add(T entity)
        {
            using (var context = new DummyDbContext())
            {
                context.Set<T>().Add(entity);
                Save();
            }
        }

        public void Delete(T entity)
        {
            using(var context = new DummyDbContext())
            {
                context.Set<T>().Remove(entity);
                Save();
            }
        }

        public IEnumerable<T> GetAll()
        {
            using(var context = new DummyDbContext())
            {
                List<T> data = new List<T>();

                foreach (var item in context.Set<T>())
                    data.Add(item);

                return data;
            }
        }

        public T GetById(int identifier)
        {
            using(var context = new DummyDbContext())
            {
                return context.Set<T>().Find(identifier);
            }
        }

        public void Save()
        {
            using(var context = new DummyDbContext())
            {
                context.SaveChanges();
            }
        }
    }
}
