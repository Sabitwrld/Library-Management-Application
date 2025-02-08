using Library_Management_Application.Data;
using Library_Management_Application.Entities.Common;
using Library_Management_Application.Repository.Interfaces;

namespace Library_Management_Application.Repository.Implementations
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity, new()
    {
        private readonly AppDbContext _context;

        public GenericRepository()
        {
            _context = new AppDbContext();
        }

        public void Add(T entity)
        => _context.Set<T>().Add(entity);

        public void Remove(T entity)
        => entity.IsDeleted = true;

        public List<T> GetAll()
        => _context.Set<T>()
            .Where(t => !t.IsDeleted)
            .ToList();
        

        public T GetById(int id)
        => _context.Set<T>().FirstOrDefault(x => x.Id == id);


        public int Commit()
        => _context.SaveChanges();  
    }
}
