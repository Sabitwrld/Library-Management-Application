using Library_Management_Application.Entities.Common;

namespace Library_Management_Application.Entities
{
    public class Borrower : BaseEntity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public List<Loan> Loans { get; set; }
    }
}
