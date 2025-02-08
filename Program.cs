using Library_Management_Application.Entities;
using Library_Management_Application.Service.Implemantations;
using Library_Management_Application.Service.Interfaces;

namespace Library_Management_Application
{
    internal class Program
    {
        static IBookService _bookService = new BookService();
        static IAuthorService _authorService = new AuthorService();
        static IBorrowerService _borrowerService = new BorrowerService();
        static ILoanService _loanService = new LoanService();
        static ILoanItemService _loanItemService = new LoanItemService();
        static void Main(string[] args)
        {


            bool menu = false;
            while (!menu)
            {
                Console.Clear();
                Console.WriteLine("\nLibrary Management Application");
                Console.WriteLine("1 - Author actions");
                Console.WriteLine("2 - Book actions");
                Console.WriteLine("3 - Borrower actions");
                Console.WriteLine("4 - Borrow Book");
                Console.WriteLine("5 - Return Book");
                Console.WriteLine("6 - Most Borrowed Book");
                Console.WriteLine("7 - Late Borrowers");
                Console.WriteLine("8 - Borrower History");
                Console.WriteLine("9 - Filter Books by Title");
                Console.WriteLine("10 - Filter Books by Author");
                Console.WriteLine("0 - Exit");
                Console.Write("Choose: ");

                string option = Console.ReadLine();
                switch (option)
                {
                    case "1":
                        AuthorActions();
                        break;
                    case "2":
                        BookActions();
                        break;
                    case "3":
                        BorrowerActions();
                        break;
                    case "4":
                        BorrowBook();
                        break;
                    case "5":
                        ReturnBook();
                        break;
                    case "6":
                        MostBorrowedBook();
                        break;
                    case "7":
                        LateBorrowers();
                        break;
                    case "8":
                        BorrowerHistory();
                        break;
                    case "9":
                        FilterBooksByTitle();
                        break;
                    case "10":
                        FilterBooksByAuthor();
                        break;
                    case "0":
                        menu=true;
                        break;
                    default:
                        Console.WriteLine("Invalid option, try again.");
                        break;
                }
                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
            }

            #region AuthorActions
            static void AuthorActions()
            {
                Console.WriteLine("\n1 - List All Authors");
                Console.WriteLine("2 - Create Author");
                Console.WriteLine("3 - Edit Author");
                Console.WriteLine("4 - Delete Author");
                Console.WriteLine("0 - Exit");
                Console.Write("Choose: ");

                string option = Console.ReadLine();
                switch (option)
                {
                    case "1":
                        var authors = _authorService.GetAll();
                        foreach (var author in authors)
                        {
                            Console.WriteLine($"ID: {author.Id}, Name: {author.Name}");
                        }
                        break;
                    case "2":
                        Console.Write("Enter author name: ");
                        string name = Console.ReadLine();
                        _authorService.Create(new Author { Name = name });
                        Console.WriteLine("Author created successfully.");
                        break;
                    case "3":
                        Console.Write("Enter author ID: ");
                        if (!int.TryParse(Console.ReadLine(), out int id)) return;
                        Console.Write("Enter new name: ");
                        string newName = Console.ReadLine();
                        _authorService.Update(id, new Author { Name = newName });
                        Console.WriteLine("Author updated successfully.");
                        break;
                    case "4":
                        Console.Write("Enter author ID: ");
                        if (!int.TryParse(Console.ReadLine(), out int deleteId)) return;
                        _authorService.Delete(deleteId);
                        Console.WriteLine("Author deleted successfully.");
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Invalid option, try again.");
                        break;
                }
            }
            #endregion

            #region BookActions
            static void BookActions()
            {
                while (true)
                {
                    Console.Clear();
                    Console.WriteLine("\nBook Actions");
                    Console.WriteLine("1 - List All Books");
                    Console.WriteLine("2 - Create Book (Requires Author)");
                    Console.WriteLine("3 - Edit Book");
                    Console.WriteLine("4 - Delete Book");
                    Console.WriteLine("0 - Exit");
                    Console.Write("Choose: ");

                    string option = Console.ReadLine();
                    if (option == "0") return;

                    switch (option)
                    {
                        case "1":
                            ListAllBooks();
                            break;
                        case "2":
                            CreateBook();
                            break;
                        case "3":
                            EditBook();
                            break;
                        case "4":
                            DeleteBook();
                            break;
                        default:
                            Console.WriteLine("Invalid option, try again.");
                            break;
                    }

                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                }
            }

            static void ListAllBooks()
            {
                try
                {
                    var books = _bookService.GetAll();
                    if (books is null)
                    {
                        Console.WriteLine("No books available.");
                        return;
                    }

                    Console.WriteLine("\nAvailable Books:");
                    foreach (var book in books)
                    {
                        Console.WriteLine($"ID: {book.Id}, Title: {book.Title}, Year: {book.PublishedYear}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }

            static void CreateBook()
            {
                try
                {
                    Console.Write("Enter book title: ");
                    string title = Console.ReadLine()?.Trim();
                    if (string.IsNullOrWhiteSpace(title))
                    {
                        Console.WriteLine("Error: Title cannot be empty.");
                        return;
                    }

                    Console.Write("Enter book description: ");
                    string desc = Console.ReadLine()?.Trim();
                    if (string.IsNullOrWhiteSpace(desc))
                    {
                        Console.WriteLine("Error: Description cannot be empty.");
                        return;
                    }

                    Console.Write("Enter published year: ");
                    if (!int.TryParse(Console.ReadLine(), out int year) || year < 1000 || year > DateTime.Now.Year)
                    {
                        Console.WriteLine("Error: Invalid year format.");
                        return;
                    }

                    Console.Write("Enter author ID: ");
                    if (!int.TryParse(Console.ReadLine(), out int authorId) || authorId < 0)
                    {
                        Console.WriteLine("Error: Invalid author ID format.");
                        return;
                    }

                    var author = _authorService.GetById(authorId);
                    if (author is null)
                    {
                        Console.WriteLine($"Error: Author with ID {authorId} does not exist. Cannot create book.");
                        return;
                    }

                    var book = new Book
                    {
                        Title = title,
                        Desc = desc,
                        PublishedYear = year,
                        Authors = new List<Author> { author }
                    };

                    _bookService.Create(book);
                    Console.WriteLine("Book created successfully.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"ERROR: {ex.Message}");
                }
            }




            static void EditBook()
            {
                try
                {
                    Console.Write("Enter book ID: ");
                    if (!int.TryParse(Console.ReadLine(), out int id))
                    {
                        Console.WriteLine("Invalid ID format.");
                        return;
                    }

                    var book = _bookService.GetById(id);
                    if (book is null)
                    {
                        Console.WriteLine("Book not found.");
                        return;
                    }

                    Console.Write("Enter new title: ");
                    string title = Console.ReadLine();

                    Console.Write("Enter new description: ");
                    string desc = Console.ReadLine();

                    Console.Write("Enter new published year: ");
                    int.TryParse(Console.ReadLine(), out int year);

                    if (!string.IsNullOrWhiteSpace(title))
                    {
                        book.Title = title;
                    }

                    if (!string.IsNullOrWhiteSpace(desc))
                    {
                        book.Desc = desc;
                    }

                    if (year > 0)
                    {
                        book.PublishedYear = year;
                    }

                    _bookService.Update(id, book);
                    Console.WriteLine("Book updated successfully.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }


            static void DeleteBook()
            {
                try
                {
                    Console.Write("Enter book ID: ");
                    if (!int.TryParse(Console.ReadLine(), out int id))
                    {
                        Console.WriteLine("Invalid ID format.");
                        return;
                    }

                    var book = _bookService.GetById(id);
                    if (book is null)
                    {
                        Console.WriteLine("Error: Book not found.");
                        return;
                    }

                    _bookService.Delete(id);
                    Console.WriteLine("Book deleted successfully.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
            #endregion

            #region BorrowerActions
            static void BorrowerActions()
            {
                while (true)
                {
                    Console.Clear();
                    Console.WriteLine("\nBorrower Actions");
                    Console.WriteLine("1 - List All Borrowers");
                    Console.WriteLine("2 - Create Borrower");
                    Console.WriteLine("3 - Edit Borrower");
                    Console.WriteLine("4 - Delete Borrower");
                    Console.WriteLine("0 - Exit");
                    Console.Write("Choose: ");

                    string option = Console.ReadLine();
                    if (option == "0") return;

                    switch (option)
                    {
                        case "1":
                            ListAllBorrowers();
                            break;
                        case "2":
                            CreateBorrower();
                            break;
                        case "3":
                            EditBorrower();
                            break;
                        case "4":
                            DeleteBorrower();
                            break;
                        default:
                            Console.WriteLine("Invalid option, try again.");
                            break;
                    }

                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                }
            }

            static void ListAllBorrowers()
            {
                try
                {
                    var borrowers = _borrowerService.GetAll();
                    if (borrowers is null)
                    {
                        Console.WriteLine("No borrowers available.");
                        return;
                    }

                    Console.WriteLine("\nBorrowers List:");
                    foreach (var borrower in borrowers)
                    {
                        Console.WriteLine($"ID: {borrower.Id}, Name: {borrower.Name}, Email: {borrower.Email}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }

            static void CreateBorrower()
            {
                try
                {
                    Console.Write("Enter borrower name: ");
                    string name = Console.ReadLine();
                    Console.Write("Enter borrower email: ");
                    string email = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(email))
                    {
                        Console.WriteLine("Name and Email cannot be empty.");
                        return;
                    }

                    var borrower = new Borrower
                    {
                        Name = name,
                        Email = email
                    };

                    _borrowerService.Create(borrower);
                    Console.WriteLine("Borrower created successfully.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }

            static void EditBorrower()
            {
                try
                {
                    Console.Write("Enter borrower ID: ");
                    if (!int.TryParse(Console.ReadLine(), out int id))
                    {
                        Console.WriteLine("Invalid ID format.");
                        return;
                    }

                    var borrower = _borrowerService.GetById(id);
                    if (borrower is null)
                    {
                        Console.WriteLine("Borrower not found.");
                        return;
                    }

                    Console.Write("Enter new name: ");
                    string name = Console.ReadLine();

                    Console.Write("Enter new email: ");
                    string email = Console.ReadLine();

                    if (!string.IsNullOrWhiteSpace(name))
                    {
                        borrower.Name = name;
                    }

                    if (!string.IsNullOrWhiteSpace(email))
                    {
                        borrower.Email = email;
                    }

                    _borrowerService.Update(id, borrower);
                    Console.WriteLine("Borrower updated successfully.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }

            static void DeleteBorrower()
            {
                try
                {
                    Console.Write("Enter borrower ID: ");
                    if (!int.TryParse(Console.ReadLine(), out int id))
                    {
                        Console.WriteLine("Invalid ID format.");
                        return;
                    }

                    var borrower = _borrowerService.GetById(id);
                    if (borrower is null)
                    {
                        Console.WriteLine("Error: Borrower not found.");
                        return;
                    }

                    _borrowerService.Delete(id);
                    Console.WriteLine("Borrower deleted successfully.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
            #endregion

            #region BorrowBook
            static void BorrowBook()
            {
                try
                {
                    List<LoanItem> selectedBooks = new List<LoanItem>();
                    Borrower selectedBorrower = null;

                    while (true)
                    {
                        Console.Clear();
                        Console.WriteLine("Available Books:\n");

                        var books = _bookService.GetAll();
                        if (books is null || books.Count == 0)
                        {
                            Console.WriteLine("No books available.");
                            return;
                        }

                        foreach (var book in books)
                        {
                            string availability = book.IsBorrowed ? "Not Available" : "Available";
                            Console.WriteLine($"ID: {book.Id}, Title: {book.Title} ({availability})");
                        }

                        Console.WriteLine("\n1 - Select Book");
                        Console.WriteLine("2 - Select Borrower");
                        Console.WriteLine("3 - Confirm Borrow");
                        Console.WriteLine("0 - Exit");

                        Console.Write("\nChoose: ");
                        string option = Console.ReadLine();

                        if (option == "0") return;

                        if (option == "1")
                        {
                            Console.Write("Enter Book ID to borrow: ");
                            if (!int.TryParse(Console.ReadLine(), out int bookId) || bookId < 0)
                            {
                                Console.WriteLine("Invalid book ID.");
                                continue;
                            }

                            var book = _bookService.GetById(bookId);
                            if (book == null)
                            {
                                Console.WriteLine("Error: Book not found.");
                                continue;
                            }

                            if (book.IsBorrowed)
                            {
                                Console.WriteLine("Error: This book is already borrowed.");
                                continue;
                            }

                            selectedBooks.Add(new LoanItem { BookId = bookId });
                            book.IsBorrowed = true;
                            _bookService.Update(bookId, book);
                            Console.WriteLine($"Book '{book.Title}' added to borrow list.");
                        }
                        else if (option == "2")
                        {
                            Console.Write("\nEnter Borrower ID: ");
                            if (!int.TryParse(Console.ReadLine(), out int borrowerId) || borrowerId < 0)
                            {
                                Console.WriteLine("Invalid borrower ID.");
                                continue;
                            }

                            selectedBorrower = _borrowerService.GetById(borrowerId);
                            if (selectedBorrower is null)
                            {
                                Console.WriteLine("Error: Borrower not found.");
                                continue;
                            }

                            Console.WriteLine($"Borrower '{selectedBorrower.Name}' selected.");
                        }
                        else if (option == "3")
                        {
                            if (selectedBorrower is null || selectedBooks.Count == 0)
                            {
                                Console.WriteLine("Error: Select a borrower and at least one book.");
                                continue;
                            }

                            var newLoan = new Loan
                            {
                                BorrowerId = selectedBorrower.Id,
                                LoanDate = DateTime.UtcNow,
                                MustReturnDate = DateTime.UtcNow.AddDays(15),
                                LoanItems = selectedBooks
                            };

                            _loanService.Create(newLoan);
                            Console.WriteLine("\nLoan successfully created! Books borrowed.");
                            return;
                        }
                        else
                        {
                            Console.WriteLine("Invalid option, try again.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"ERROR: {ex.Message}");
                }
            }


            #endregion

            #region ReturnBook
            static void ReturnBook()
            {
                try
                {
                    Console.Write("Enter Borrower ID: ");
                    if (!int.TryParse(Console.ReadLine(), out int borrowerId) || borrowerId < 0)
                    {
                        Console.WriteLine("Invalid ID format.");
                        return;
                    }

                    var activeLoan = _loanService.GetAll()
                        .FirstOrDefault(l => l.BorrowerId == borrowerId && l.ReturnDate == null);

                    if (activeLoan == null)
                    {
                        Console.WriteLine("No active loan found for this borrower.");
                        return;
                    }

                    activeLoan.ReturnDate = DateTime.UtcNow.AddHours(4);
                    _loanService.Update(activeLoan.Id, activeLoan);

                    Console.WriteLine("Book returned successfully.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"ERROR: {ex.Message}");
                }
            }


            #endregion

            #region MostBorrowedBook
            static void MostBorrowedBook()
            {
                try
                {
                    var loans = _loanService.GetAll();
                    if (loans is null || loans.Count == 0)
                    {
                        Console.WriteLine("No books have been borrowed yet.");
                        return;
                    }

                    var mostBorrowed = loans
                        .SelectMany(l => l.LoanItems)
                        .GroupBy(i => i.BookId)
                        .OrderByDescending(g => g.Count())
                        .FirstOrDefault();

                    if (mostBorrowed == null)
                    {
                        Console.WriteLine("No books found.");
                        return;
                    }

                    var book = _bookService.GetById(mostBorrowed.Key);
                    Console.WriteLine($"Most Borrowed Book: {book.Title} ({mostBorrowed.Count()} times)");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"ERROR: {ex.Message}");
                }
            }

            #endregion

            #region LateBorrowers
            static void LateBorrowers()
            {
                var lateLoans = _loanService.GetAll()
                .Where(l => l.MustReturnDate < DateTime.UtcNow.AddHours(4) && l.ReturnDate is null)
                .ToList();


                if (lateLoans is null)
                {
                    Console.WriteLine("No late borrowers found.");
                    return;
                }

                Console.WriteLine("Late Borrowers:");
                foreach (var loan in lateLoans)
                {
                    var borrower = _borrowerService.GetById(loan.BorrowerId);
                    if (borrower is not null)
                    {
                        Console.WriteLine($"ID: {borrower.Id}, Name: {borrower.Name}");
                    }
                }
            }
            #endregion

            #region BorrowerHistory
            static void BorrowerHistory()
            {
                Console.Write("Enter Borrower ID: ");
                if (!int.TryParse(Console.ReadLine(), out int borrowerId))
                {
                    Console.WriteLine("Invalid ID.");
                    return;
                }

                var loans = _loanService.GetAll().Where(l => l.BorrowerId == borrowerId).ToList();
                if (loans is null)
                {
                    Console.WriteLine("No history found for this borrower.");
                    return;
                }

                Console.WriteLine($"Borrower History (ID: {borrowerId}):");
                foreach (var loan in loans)
                {
                    foreach (var item in loan.LoanItems)
                    {
                        var book = _bookService.GetById(item.BookId);
                        Console.WriteLine($"{book.Title}");
                    }
                }
            }
            #endregion

            #region FilterBooksByTitle
            static void FilterBooksByTitle()
            {
                Console.Write("Enter book title: ");
                string title = Console.ReadLine().Trim().ToLower();

                var books = _bookService.GetAll().Where(b => b.Title.ToLower().Contains(title)).ToList();
                if (books is null)
                {
                    Console.WriteLine("No books found.");
                    return;
                }

                Console.WriteLine("Matching Books:");
                foreach (var book in books)
                {
                    Console.WriteLine($"ID: {book.Id}, Title: {book.Title}");
                }
            }
            #endregion

            #region FilterBooksByAuthor
            static void FilterBooksByAuthor()
            {
                Console.Write("Enter author name: ");
                string authorName = Console.ReadLine().Trim().ToLower();

                var books = _bookService.GetAll()
                    .Where(b => b.Authors.Any(a => a.Name.ToLower().Contains(authorName)))
                    .ToList();

                if (books is null)
                {
                    Console.WriteLine("No books found.");
                    return;
                }

                Console.WriteLine("Books by Author:");
                foreach (var book in books)
                {
                    Console.WriteLine($"ID: {book.Id}, Title: {book.Title}");
                }
            }
            #endregion
        }

    }
}



