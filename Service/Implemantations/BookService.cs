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
                throw new EntityNotFoundException($"{book} is not exists");

            _bookRepository.Add(book);
            _bookRepository.Commit();
        }

        public void Delete(int? id)
        {
            if (id is null || id < 0)
                throw new EntityNotFoundException($"{id} is not exists");

            var existsBook = _bookRepository.GetById((int)id);

            if (existsBook is null)
                throw new EntityNotFoundException($"{existsBook} is not exists");

            _bookRepository.Remove(existsBook);
            _bookRepository.Commit();
        }

        public List<Book> GetAll()
        {
            return _bookRepository.GetAll().ToList();
        }

        public Book GetById(int? id)
        {
            if (id is null || id < 0)
                throw new EntityNotFoundException($"{id} is not exists");

            var existsBook = _bookRepository.GetById((int)id);

            if (existsBook is null)
                throw new EntityNotFoundException($"{existsBook} is not exists");
            return existsBook;
        }

        public void Update(int? id, Book book)
        {
            if (id is null || id < 0)
                throw new EntityNotFoundException("Invalid book ID");

            var existingBook = _bookRepository.GetById((int)id);
            if (existingBook is null)
                throw new EntityNotFoundException("Book does not exist");

            existingBook.Title = book.Title;
            existingBook.Desc = book.Desc;
            existingBook.PublishedYear = book.PublishedYear;
            existingBook.Authors = book.Authors;
            _bookRepository.Update((int)id, existingBook);
            _bookRepository.Commit();
        }
    }
}
