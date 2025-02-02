using Library_Management_Application.Data;
using Library_Management_Application.Entities;
using Library_Management_Application.PB503LibraryExceptions;
using Library_Management_Application.Repository.Implementations;
using Library_Management_Application.Repository.Interfaces;
using Library_Management_Application.Service.Interfaces;

namespace Library_Management_Application.Service.Implemantations
{
    public class BorrewerService : IBorrewerService
    {
        private readonly IBorrowerRepository _borrewerRepository;
        public BorrewerService()
        {
            _borrewerRepository = new BorrowerRepository();
        }

        public void Create(Borrower borrower)
        {
            if (borrower is null)
                throw new EntityNotFoundException($"{borrower} is not exists");

            if (!string.IsNullOrWhiteSpace(borrower.Name))
                throw new EntityNotFoundException($"{borrower.Name} is not exists");       

            if (!string.IsNullOrWhiteSpace(borrower.Email))
                throw new EntityNotFoundException($"{borrower.Email} is not exists");

            _borrewerRepository.Add(borrower);
            _borrewerRepository.Commit();
        }

        public void Delete(int? id)
        {
            if (id is null || id < 0)
                throw new EntityNotFoundException($"{id} is not exists");

            var existsBorrower = _borrewerRepository.GetById((int)id);

            if (existsBorrower is null)
                throw new EntityNotFoundException($"{existsBorrower} is not exists");

            _borrewerRepository.Remove(existsBorrower);
            _borrewerRepository.Commit();
        }

        public List<Borrower> GetAll()
        {
            return _borrewerRepository.GetAll().ToList();
        }

        public Borrower GetById(int? id)
        {
            if (id is null || id < 0)
                throw new EntityNotFoundException($"{id} is not exists");

            var existsBorrower = _borrewerRepository.GetById((int)id);
            if (existsBorrower is null)
                throw new EntityNotFoundException($"{existsBorrower} is not exists");
            return existsBorrower;
        }

        public void Update(int? id, Borrower borrower)
        {
            if (id is null || id < 0)
                throw new EntityNotFoundException($"{id} is not exists");

            var existsBorrewer = _borrewerRepository.GetById((int)id);

            if (borrower is null)
                throw new EntityNotFoundException($"{borrower} is not exists");

            if (!string.IsNullOrWhiteSpace(borrower.Name))
                throw new EntityNotFoundException($"{borrower.Name} is not exists");

            if (!string.IsNullOrWhiteSpace(borrower.Email))
                throw new EntityNotFoundException($"{borrower.Email} is not exists");

            existsBorrewer.Name = borrower.Name;
            existsBorrewer.Email = borrower.Email;
            existsBorrewer.Loans = borrower.Loans;
            _borrewerRepository.Update((int)id, existsBorrewer);
        }
    }
}
