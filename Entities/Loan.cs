using Library_Management_Application.Entities.Common;
using Microsoft.EntityFrameworkCore;

namespace Library_Management_Application.Entities
{
    public class Loan : BaseEntity
    {
        public int BorrowerId { get; set; }
        public Borrower Borrower { get; set; }
        public DateTime LoanDate { get; set; }
        public DateTime MustReturnDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public DbSet<LoanItem> LoanItems { get; set; }
    }
}
