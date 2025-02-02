using Library_Management_Application.Data;
using Library_Management_Application.Entities;
using Library_Management_Application.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_Management_Application.Repository.Implementations
{
    public class LoanItemRepository : GenericRepository<LoanItem>, ILoanItemRepository
    {

    }
}
