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
                throw new EntityNotFoundException("Author cannot be null");

            if (string.IsNullOrWhiteSpace(author.Name))
                throw new EntityNotFoundException("Author name cannot be empty");

            var newAuthor = new Author
            {
                Name = author.Name,
                CreatedAt = DateTime.UtcNow.AddHours(4),
                UpdatedAt = DateTime.UtcNow.AddHours(4)
            };

            _authorRepository.Add(newAuthor);
            _authorRepository.Commit();
        }

        public void Delete(int? id)
        {
            if (id is null || id < 0)
                throw new EntityNotFoundException("Invalid author ID");

            var existsAuthor = _authorRepository.GetById((int)id);
            if (existsAuthor is null)
                throw new EntityNotFoundException($"Author with ID {id} not found");

            _authorRepository.Remove(existsAuthor);
            _authorRepository.Commit();
        }

        public List<Author> GetAll()
        {
            return _authorRepository
                .GetAll()
                .ToList();
        }

        public Author GetById(int? id)
        {
            if (id is null || id < 0)
                throw new EntityNotFoundException("Invalid author ID");

            var existsAuthor = _authorRepository.GetById((int)id);

            if (existsAuthor is null)
                throw new EntityNotFoundException($"Author with ID {id} not found");

            return existsAuthor;
        }

        public void Update(int? id, Author author)
        {
            if (id is null || id < 0)
                throw new EntityNotFoundException("Invalid author ID");

            var existsAuthor = _authorRepository.GetById((int)id);
            if (existsAuthor is null)
                throw new EntityNotFoundException($"Author with ID {id} not found");

            if (author is null)
                throw new EntityNotFoundException("Author cannot be null");

            if (string.IsNullOrWhiteSpace(author.Name))
                throw new EntityNotFoundException("Author name cannot be empty");

            existsAuthor.Name = author.Name;
            existsAuthor.UpdatedAt = DateTime.UtcNow.AddHours(4);

            _authorRepository.Commit();
        }
    }
}
