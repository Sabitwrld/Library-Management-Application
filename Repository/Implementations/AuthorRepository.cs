using Library_Management_Application.Data;
using Library_Management_Application.Entities;
using Library_Management_Application.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Library_Management_Application.Repository.Implementations
{
    public class AuthorRepository : GenericRepository<Author>, IAuthorRepository
    {
        private readonly AppDbContext _context;

        public AuthorRepository()
        {
            _context = new AppDbContext();
        }

        public Author GetById(int id)
        {
            var author = _context.Authors
            .Include(a => a.Books)
            .FirstOrDefault(a => a.Id == id);

            if (author is null)
            {
                throw new Exception($"Author with ID {id} not found."); 
            }

            return author;
        }
    }
}
