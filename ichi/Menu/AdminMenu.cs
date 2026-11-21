using ichi.Models;
using ichi.Services;
using Spectre.Console;

namespace ichi.Menu
{
    internal class AdminMenu
    {
        private readonly BookService _bookService;
        private readonly UserService _userService;
        private readonly PurchaseService _orderService;

        public AdminMenu(BookService bookService, UserService userService, PurchaseService orderService)
        {
            _bookService = bookService;
            _userService = userService;
            _orderService = orderService;
        }

        public void Show(User user)
        {
            string choice = "";
            while (user.IsLogged == true && user.Role == "admin" && choice != "0. Exit")
            {

                choice = AnsiConsole.Prompt(new SelectionPrompt<string>()
                .Title("1. Show books, 2. Add book, 3. Update book, 4. Delete book, 5. Add admin, 0. Exit")
                .AddChoices("1. Show books", "2. Add book", "3. Update book", "4. Delete book", "5. Add admin", "0. Exit"));
                switch (choice)
                {
                    case "1. Show books":
                        ShowBooks();
                        break;

                    case "2. Search books":
                        AddBook();
                        break;

                    case "3. Buy books":
                        UpdateBook();
                        break;

                    case "4. Delete book":
                        DeleteBook();
                        break;

                    case "5. Add admin":
                        AddAdmin();
                        break;

                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;

                }
            }
            ;
        }
                //while (true)
                //{
                //    Console.WriteLine("\n--- Admin menu ---");
                //    Console.WriteLine("1. Show books");
                //    Console.WriteLine("2. Add book");
                //    Console.WriteLine("3. Update book");
                //    Console.WriteLine("4. Delete book");
                //    Console.WriteLine("5. Add admin");
                //    //Console.WriteLine("5. Show all orders");
                //    Console.WriteLine("0. Logout");
                //    Console.Write("Choose: ");
                //    string choice = Console.ReadLine() ?? "";

                //    try
                //    {
                //        if (choice == "1") ShowBooks();
                //        else if (choice == "2") AddBook();
                //        else if (choice == "3") UpdateBook();
                //        else if (choice == "4") DeleteBook();
                //        //else if (choice == "5") ShowOrders();
                //        else if (choice == "0") return;
                //        else Console.WriteLine("Invalid choice.");
                //    }
                //    catch (Exception error)
                //    {
                //        Console.WriteLine("Error: " + error.Message);
                //    }
                //}
            //}



        private void AddAdmin()
        {
            var users = _userService.GetAllUsers();

            Console.WriteLine("Search user by id");
            int ids = int.Parse(Console.ReadLine() ?? "0");

            foreach (var user in users)
            {
                if (user.Id == ids)
                {
                    string choice = "";
                    Console.WriteLine($"User found: {user.Name}");
                    Console.WriteLine($"Do you want to make them an admin?");
                    while (choice != "2. No")
                    {
                        choice = AnsiConsole.Prompt(new SelectionPrompt<string>()
                .Title("1. Yes 2. No")
                .AddChoices("1. Yes", "2. No"));
                        //choice = Console.ReadLine();
                        switch (choice)
                        {
                            case "1. Yes":
                                user.Role = "admin";
                                break;

                            case "2. No":
                                
                                break;

                            default:
                                Console.WriteLine("Invalid choice. Please try again.");
                                break;

                        }
                    }
                }
            }
            
        }


        private void ShowBooks()
        {
            List<Book> books = _bookService.GetAllBooks();
            foreach (Book book in books)
                Console.WriteLine($"{book.Id}. {book.Name} | Vol: {book.Vol}. Description: {book.Description}");

            if (books.Count == 0)
                Console.WriteLine("No books found.");
        }

        private void AddBook()
        {
            Console.Write("Book name: ");
            string name = Console.ReadLine() ?? "";
            Console.Write("Book Vol: ");
            int vol = int.Parse(Console.ReadLine() ?? "0");
            Console.Write("Description: ");
            string description = Console.ReadLine() ?? "";

            _bookService.AddBook(name, vol, description);
            Console.WriteLine("Book added.");
        }

        private void UpdateBook()
        {
            int id = ReadInt("Book id: ");
            Console.Write("New name: ");
            string name = Console.ReadLine() ?? "";
            int vol = ReadInt("Book vol: ");
            Console.Write("New description: ");
            string description = Console.ReadLine() ?? "";

            if (_bookService.UpdateBook(id, name, vol, description))
                Console.WriteLine("Book updated.");
            else
                Console.WriteLine("Book was not found.");
        }

        private void DeleteBook()
        {
            int id = ReadInt("Book id: ");
            if (_bookService.DeleteBook(id))
                Console.WriteLine("Book deleted.");
            else
                Console.WriteLine("Book was not found.");
        }

        //private void ShowPurchases()
        //{
        //    List<Purchase> purchase = _purchaseService.GetAllPurchases();
        //    foreach (Purchase purchase in purchases)
        //        Console.WriteLine($"{purchase.Id}. User: {purchase.UserId}, Book: {purchase.BookName}, Quantity: {purchase.Quantity}");

        //    if (purchases.Count == 0)
        //        Console.WriteLine("No purchases found.");
        //}

        private static int ReadInt(string message)
        {
            Console.Write(message);
            if (int.TryParse(Console.ReadLine(), out int number))
                return number;
            throw new ArgumentException("Please enter a number.");
        }
    }
}
