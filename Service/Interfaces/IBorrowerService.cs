using Library_Management_Application.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_Management_Application.Service.Interfaces
{
    public interface IBorrowerService
    {
        void Create(Borrower borrower);
        void Update(int? id, Borrower borrower);
        void Delete(int? id);
        Borrower GetById(int? id);
        List<Borrower> GetAll();
    }
}
