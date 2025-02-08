using Library_Management_Application.Data;
using Library_Management_Application.Entities;
using Library_Management_Application.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_Management_Application.Repository.Implementations
{
    public class BookRepository : GenericRepository<Book>, IBookRepository
    {
        private readonly AppDbContext _context;

        public BookRepository()
        {
            _context = new AppDbContext();
        }
        public Book GetById(int id)
        {
            var book = _context.Books
            .Include(a => a.Authors)
            .FirstOrDefault(a => a.Id == id);

            if (book is null)
            {
                throw new Exception($"Author with ID {id} not found.");
            }

            return book;
        }
    }
}
