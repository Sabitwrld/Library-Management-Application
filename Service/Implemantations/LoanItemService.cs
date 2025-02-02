using Library_Management_Application.Data;
using Library_Management_Application.Entities;
using Library_Management_Application.PB503LibraryExceptions;
using Library_Management_Application.Repository.Implementations;
using Library_Management_Application.Repository.Interfaces;
using Library_Management_Application.Service.Interfaces;

namespace Library_Management_Application.Service.Implemantations
{
    public class LoanItemService : ILoanItemService
    {
        private readonly ILoanItemRepository _loanItemRepository;
        private readonly AppDbContext _context;
        public LoanItemService()
        {
            _loanItemRepository = new LoanItemRepository();
        }

        public void Create(LoanItem loanItem)
        {
            if (loanItem is null)
                throw new EntityNotFoundException("LoanItem entity cannot be null");

            var existsBook = _context.Books.FirstOrDefault(x => x.Id == loanItem.BookId);
            if (existsBook is null)
                throw new EntityNotFoundException("Book does not exist");

            var existsLoan = _context.Loans.FirstOrDefault(x => x.Id == loanItem.Id);
            if (existsLoan is null)
                throw new EntityNotFoundException("Loan does not exist");

            _loanItemRepository.Add(loanItem);
            _loanItemRepository.Commit();
        }

        public void Delete(int? id)
        {
            if (id is null || id < 0)
                throw new EntityNotFoundException("Invalid loan item ID");

            var loanItem = _loanItemRepository.GetById((int)id);
            if (loanItem is null)
                throw new EntityNotFoundException("Loan item does not exist");

            _loanItemRepository.Remove(loanItem);
            _loanItemRepository.Commit();
        }

        public List<LoanItem> GetAll()
        {
            return _loanItemRepository.GetAll().ToList();
        }

        public LoanItem GetById(int? id)
        {
            if (id is null || id < 0)
                throw new EntityNotFoundException($"{id} is not exists");

            var existsLoanItem = _loanItemRepository.GetById((int)id);

            if (existsLoanItem is null)
                throw new EntityNotFoundException($"{existsLoanItem} is not exists");
            return existsLoanItem;
        }

        public void Update(int? id, LoanItem loanItem)
        {
            if (id is null || id < 0)
                throw new EntityNotFoundException("Invalid loan item ID");

            var existingLoanItem = _loanItemRepository.GetById((int)id);
            if (existingLoanItem is null)
                throw new EntityNotFoundException("Loan item does not exist");

            existingLoanItem.BookId = loanItem.BookId;
            existingLoanItem.LoanId = loanItem.LoanId;
            _loanItemRepository.Update((int)id, existingLoanItem);
            _loanItemRepository.Commit();
        }
    }
}
