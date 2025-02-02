using Library_Management_Application.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
