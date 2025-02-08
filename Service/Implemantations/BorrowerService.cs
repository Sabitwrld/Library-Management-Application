using Library_Management_Application.Data;
using Library_Management_Application.Entities;
using Library_Management_Application.PB503LibraryExceptions;
using Library_Management_Application.Repository.Implementations;
using Library_Management_Application.Repository.Interfaces;
using Library_Management_Application.Service.Interfaces;

namespace Library_Management_Application.Service.Implemantations
{
    public class BorrowerService : IBorrowerService
    {
        private readonly IBorrowerRepository _borrowerRepository;
        public BorrowerService()
        {
            _borrowerRepository = new BorrowerRepository();
        }

        public void Create(Borrower borrower)
        {
            if (borrower is null)
                throw new EntityNotFoundException("Borrower cannot be null");

            if (string.IsNullOrWhiteSpace(borrower.Name))
                throw new EntityNotFoundException("Borrower name cannot be empty");

            if (string.IsNullOrWhiteSpace(borrower.Email))
                throw new EntityNotFoundException("Borrower email cannot be empty");

            var newBorrower = new Borrower
            {
                Name = borrower.Name,
                Email = borrower.Email,
                Loans = borrower.Loans,
                CreatedAt = DateTime.UtcNow.AddHours(4),
                UpdatedAt = DateTime.UtcNow.AddHours(4)
            };

            _borrowerRepository.Add(newBorrower);
            _borrowerRepository.Commit();
        }

        public void Delete(int? id)
        {
            if (id is null || id < 0)
                throw new EntityNotFoundException("Invalid borrower ID");

            var existingBorrower = _borrowerRepository.GetById((int)id);
            if (existingBorrower is null)
                throw new EntityNotFoundException($"Borrower with ID {id} not found");

            _borrowerRepository.Remove(existingBorrower);
            _borrowerRepository.Commit();
        }

        public List<Borrower> GetAll()
        {
            return _borrowerRepository.GetAll().ToList();
        }

        public Borrower GetById(int? id)
        {
            if (id is null || id < 0)
                throw new EntityNotFoundException("Invalid borrower ID");

            var existingBorrower = _borrowerRepository.GetById((int)id);
            if (existingBorrower is null)
                throw new EntityNotFoundException($"Borrower with ID {id} not found");

            return existingBorrower;
        }

        public void Update(int? id, Borrower borrower)
        {
            if (id is null || id < 0)
                throw new EntityNotFoundException("Invalid borrower ID");

            var existingBorrower = _borrowerRepository.GetById((int)id);
            if (existingBorrower is null)
                throw new EntityNotFoundException($"Borrower with ID {id} not found");

            if (borrower is null)
                throw new EntityNotFoundException("Borrower cannot be null");

            if (string.IsNullOrWhiteSpace(borrower.Name))
                throw new EntityNotFoundException("Borrower name cannot be empty");

            if (string.IsNullOrWhiteSpace(borrower.Email))
                throw new EntityNotFoundException("Borrower email cannot be empty");

            existingBorrower.Name = borrower.Name;
            existingBorrower.Email = borrower.Email;
            existingBorrower.Loans = borrower.Loans;
            existingBorrower.UpdatedAt = DateTime.UtcNow.AddHours(4);

            _borrowerRepository.Commit();
        }
    }
}
