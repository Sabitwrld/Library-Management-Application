using Library_Management_Application.Entities;
using Library_Management_Application.Entities.Common;

namespace Library_Management_Application.Repository.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity, new()
    {
        void Add(T entity);
        void Update(int id, T entity);
        void Remove(T entity);
        List<T> GetAll();
        T GetById(int id);
        int Commit();
    }
}
