using Library_Management_Application.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_Management_Application.Service.Interfaces
{
    public interface ILoanItemService
    {
        void Create(LoanItem loanItem);
        void Update(int? id, LoanItem loanItem);
        void Delete(int? id);
        LoanItem GetById(int? id);
        List<LoanItem> GetAll();
    }
}
