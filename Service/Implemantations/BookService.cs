using Library_Management_Application.Data;
using Library_Management_Application.Entities;
using Library_Management_Application.PB503LibraryExceptions;
using Library_Management_Application.Repository.Implementations;
using Library_Management_Application.Repository.Interfaces;
using Library_Management_Application.Service.Interfaces;

namespace Library_Management_Application.Service.Implemantations
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        public BookService()
        {
            _bookRepository = new BookRepository();
        }

        public void Create(Book book)
        {
            if (book is null)
                throw new Exception("Book cannot be null.");

            if (book.Authors is null)
                book.Authors = new List<Author>();

            if (!book.Authors.Any())
                throw new Exception("A book must have at least one author.");

            var newBook = new Book
            {
                Title = book.Title,
                Desc = book.Desc,
                PublishedYear = book.PublishedYear,
                Authors = book.Authors,
                CreatedAt = DateTime.UtcNow.AddHours(4),
                UpdatedAt = DateTime.UtcNow.AddHours(4)
            };

            _bookRepository.Add(newBook);
            _bookRepository.Commit();
        }


        public void Delete(int? id)
        {
            if (id is null || id < 0)
                throw new EntityNotFoundException("Invalid book ID");

            var existsBook = _bookRepository.GetById((int)id);
            if (existsBook is null)
                throw new EntityNotFoundException($"Book with ID {id} not found");

            _bookRepository.Remove(existsBook);
            _bookRepository.Commit();
        }

        public List<Book> GetAll()
        {
            return _bookRepository
                .GetAll()
                .ToList();
        }

        public Book GetById(int? id)
        {
            if (id is null || id < 0)
                throw new EntityNotFoundException("Invalid book ID");

            var existsBook = _bookRepository.GetById((int)id);

            if (existsBook is null)
                throw new EntityNotFoundException($"Book with ID {id} not found");

            return existsBook;
        }

        public void Update(int? id, Book book)
        {
            if (id is null || id < 0)
                throw new EntityNotFoundException("Invalid book ID");

            var existingBook = _bookRepository.GetById((int)id);
            if (existingBook is null)
                throw new EntityNotFoundException($"Book with ID {id} not found");

            if (book is null)
                throw new EntityNotFoundException("Book cannot be null");

            existingBook.Title = book.Title;
            existingBook.Desc = book.Desc;
            existingBook.PublishedYear = book.PublishedYear;
            existingBook.Authors = book.Authors;
            existingBook.UpdatedAt = DateTime.UtcNow.AddHours(4);

            _bookRepository.Commit();
        }
    }
}
