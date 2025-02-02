using Library_Management_Application.Data;
using Library_Management_Application.Entities;
using Library_Management_Application.PB503LibraryExceptions;
using Library_Management_Application.Repository.Implementations;
using Library_Management_Application.Repository.Interfaces;
using Library_Management_Application.Service.Interfaces;

namespace Library_Management_Application.Service.Implemantations
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _authorRepository;
        public AuthorService()
        {
            _authorRepository = new AuthorRepository();
        }

        public void Create(Author author)
        {
            if (author is null)
                throw new EntityNotFoundException($"{author} is not exists");

            if (!string.IsNullOrWhiteSpace(author.Name))
                throw new EntityNotFoundException($"{author.Name} is not exists");

            _authorRepository.Add(author);
            _authorRepository.Commit();
        }

        public void Delete(int? id)
        {
            if (id is null || id < 0)
                throw new EntityNotFoundException($"{id} is not exists");
            
            var existsAuthor = _authorRepository.GetById((int)id);
            
            if (existsAuthor is null)
                throw new EntityNotFoundException($"{existsAuthor} is not exists");

            _authorRepository.Remove(existsAuthor);
            _authorRepository.Commit();
        }

        public List<Author> GetAll()
        {
            return _authorRepository.GetAll().ToList();
        }

        public Author GetById(int? id)
        {
            if (id is null || id < 0)
                throw new EntityNotFoundException($"{id} is not exists");

            var existsAuthor = _authorRepository.GetById((int)id);
            if (existsAuthor is null)
                throw new EntityNotFoundException($"{existsAuthor} is not exists");

            return existsAuthor;
        }

        public void Update(int? id, Author author)
        {
            if (id is null || id < 0)
                throw new EntityNotFoundException($"{id} is not exists");

            var existsAuthor = _authorRepository.GetById((int)id);

            if (author is null)
                throw new EntityNotFoundException($"{author} is not exists");

            if (!string.IsNullOrWhiteSpace(author.Name))
                throw new EntityNotFoundException($"{author.Name} is not exists");

            existsAuthor.Name = author.Name;
            existsAuthor.Books = author.Books;
            _authorRepository.Update((int)id, existsAuthor);
        }



        //private Author CheckId(int? id)
        //{
        //    if (id is null)
        //        throw new EntityNotFoundException($"{id} is not exists");
        //    var exists = _repository.GetById((int)id);
        //}
    }
}
