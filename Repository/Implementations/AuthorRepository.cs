using Library_Management_Application.Data;
using Library_Management_Application.Entities;
using Library_Management_Application.Repository.Interfaces;

namespace Library_Management_Application.Repository.Implementations
{
    public class AuthorRepository : GenericRepository<Author>, IAuthorRepository
    {
        public AuthorRepository(AppDbContext context) : base(context)
        {
        }
    }
}
