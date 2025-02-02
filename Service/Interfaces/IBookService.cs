using Library_Management_Application.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_Management_Application.Service.Interfaces
{
    public interface IBookService
    {
        void Create(Book book);
        void Update(int? id, Book book);
        void Delete(int? id);
        Book GetById(int? id);
        List<Book> GetAll();

    }
}
