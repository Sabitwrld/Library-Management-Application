using Library_Management_Application.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_Management_Application.Service.Interfaces
{
    public interface ILoanService
    {
        void Create(Loan loan);
        void Update(int? id, Loan loan);
        void Delete(int? id);
        Loan GetById(int? id);
        List<Loan> GetAll();
    }
}
