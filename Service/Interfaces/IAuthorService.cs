using Library_Management_Application.Entities;

namespace Library_Management_Application.Service.Interfaces
{
    public interface IAuthorService
    {
        void Create(Author author);
        void Update(int? id, Author author);
        void Delete(int? id);
        Author GetById(int? id);
        List<Author> GetAll();
    }
}
