using Library_Management_Application.Data;
using Library_Management_Application.Entities.Common;
using Library_Management_Application.Repository.Interfaces;

namespace Library_Management_Application.Repository.Implementations
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity, new()
    {
        private readonly AppDbContext _context;

        public GenericRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
            Commit();
        }

        public void Update(int id, T entity)
        {
            _context.Set<T>().Update(entity);
            Commit();
        }

        public void Remove(T entity)
        {
            _context.Set<T>().Remove(entity);
            Commit();
        }

        public List<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }

        public T GetById(int id)
        {
            return _context.Set<T>().FirstOrDefault(x => x.Id == id);
        }

        public int Commit()
        {
            return _context.SaveChanges();
        }
    }
}
