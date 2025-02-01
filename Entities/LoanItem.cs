using Library_Management_Application.Entities.Common;

namespace Library_Management_Application.Entities
{
    public class LoanItem : BaseEntity
    {
        public int BookId { get; set; }
        public Book Book { get; set; }
        public int LoanId { get; set; }
        public Loan Loan { get; set; }

    }
}
