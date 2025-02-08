using Library_Management_Application.Data;
using Library_Management_Application.Entities;
using Library_Management_Application.PB503LibraryExceptions;
using Library_Management_Application.Repository.Implementations;
using Library_Management_Application.Repository.Interfaces;
using Library_Management_Application.Service.Interfaces;

namespace Library_Management_Application.Service.Implemantations
{
    public class LoanService : ILoanService
    {
        private readonly ILoanRepository _loanRepository;
        private readonly IBorrowerRepository _borrowerRepository;

        public LoanService()
        {
            _loanRepository = new LoanRepository();
            _borrowerRepository = new BorrowerRepository();
        }

        public void Create(Loan loan)
        {
            if (loan is null)
                throw new EntityNotFoundException("Loan cannot be null");

            var existsBorrower = _borrowerRepository.GetById(loan.BorrowerId);
            if (existsBorrower is null)
                throw new EntityNotFoundException("Borrower does not exist");

            loan.LoanDate = DateTime.UtcNow.AddHours(4);
            loan.MustReturnDate = loan.LoanDate.AddDays(15);

            _loanRepository.Add(loan);
            _loanRepository.Commit();
        }

        public void Delete(int? id)
        {
            if (id is null || id < 0)
                throw new EntityNotFoundException("Invalid loan ID");

            var existsLoan = _loanRepository.GetById((int)id);
            if (existsLoan is null)
                throw new EntityNotFoundException($"Loan with ID {id} not found");

            _loanRepository.Remove(existsLoan);
            _loanRepository.Commit();
        }

        public List<Loan> GetAll()
        {
            return _loanRepository.GetAll().ToList();
        }

        public Loan GetById(int? id)
        {
            if (id is null || id < 0)
                throw new EntityNotFoundException("Invalid loan ID");

            var existsLoan = _loanRepository.GetById((int)id);
            if (existsLoan is null)
                throw new EntityNotFoundException($"Loan with ID {id} not found");

            return existsLoan;
        }

        public void Update(int? id, Loan loan)
        {
            if (id is null || id < 0)
                throw new EntityNotFoundException("Invalid loan ID");

            var existingLoan = _loanRepository.GetById((int)id);
            if (existingLoan is null)
                throw new EntityNotFoundException($"Loan with ID {id} not found");

            existingLoan.ReturnDate = loan.ReturnDate;
            existingLoan.LoanItems = loan.LoanItems;
            existingLoan.UpdatedAt = DateTime.UtcNow.AddHours(4);

            _loanRepository.Commit();
        }
    }
}
