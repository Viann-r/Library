using ichi.Models;
using ichi.Services;
using Spectre.Console;

namespace ichi.Menu
{
    internal class ClientMenu
    {
        private readonly BookService _bookService;
        private readonly PurchaseService _purchaseService;
        public ClientMenu(BookService bookService, PurchaseService orderService)
        {
            _bookService = bookService;
            _purchaseService = orderService;
        }
        public void ShowClientMenu(User user)
        {
            string choice = "";
            while (user.IsLogged == true && user.Role == "client" && choice != "0. Exit")
            {

                choice = AnsiConsole.Prompt(new SelectionPrompt<string>()
                .Title("1. Show books, 2. Search books, 3. Buy books, 0. Exit")
                .AddChoices("1. Show books", "2. Search books", "3. Buy books", "0. Exit"));
                switch (choice)
                {
                    case "1. Show books":
                        ShowBooks(_bookService.GetAllBooks());
                        break;

                    case "2. Search books":
                        SearchBooks();
                        break;

                    case "3. Buy books":
                        PlacePurchase(user);
                        break;

                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;

                }
                //Console.WriteLine("\n--- Client menu ---");
                //Console.WriteLine("1. Show books");
                //Console.WriteLine("2. Search book");
                //Console.WriteLine("3. Place order");
                //Console.WriteLine("4. My orders");
                //Console.WriteLine("0. Logout");
                //Console.Write("Choose: ");
                //string choice = Console.ReadLine() ?? "";

                //try
                //{
                //    if (choice == "1") ShowBooks(_bookService.GetAllBooks());
                //    else if (choice == "2") SearchBooks();
                //    else if (choice == "3") PlacePurchase(user);
                //    else if (choice == "4") ShowMyPurchases(user);
                //    else if (choice == "0") return;
                //    else Console.WriteLine("Invalid choice.");
                //}
                //catch (Exception error)
                //{
                //    Console.WriteLine("Error: " + error.Message);
                //}
            }
        }

        private void SearchBooks()
        {
            Console.Write("Search text: ");
            string text = Console.ReadLine() ?? "";
            ShowBooks(_bookService.SearchBooks(text));
        }

        private void PlacePurchase(User user)
        {
            List<Book> books = _bookService.GetAllBooks();
            if (books.Count == 0)
            {
                Console.WriteLine("The book list is empty.");
                return;
            }

            ShowBooks(books);
            int bookId = ReadInt("Book id: ");
            Book? book = _bookService.GetBookById(bookId);
            if (book == null)
            {
                Console.WriteLine("Book was not found.");
                return;
            }
            
            int vol = ReadInt("Vol: ");
            Purchase purchase = _purchaseService.CreatePurchase(user, book, vol);
            //Console.WriteLine("Order created. Order id: " + order.Id);
        }

        //private void ShowMyPurchases(User user)
        //{
        //    List<Purchase> purchases = _purchaseService.GetPurchasesForUser(user.Id);
        //    foreach (Purchase purchase in purchases)
        //        Console.WriteLine($"{purchase.Id}. {purchase.BookName}, quantity: {purchase.Vol}.");

        //    if (purchases.Count == 0)
        //        Console.WriteLine("You have no purchases.");
        //}

        private static void ShowBooks(List<Book> books)
        {
            foreach (Book book in books)
                Console.WriteLine($"{book.Id}. {book.Name} | vol: {book.Vol}. Description: {book.Description}");

            if (books.Count == 0)
                Console.WriteLine("No books found.");
        }

        private static int ReadInt(string message)
        {
            Console.Write(message);
            if (int.TryParse(Console.ReadLine(), out int number))
                return number;
            throw new ArgumentException("Please enter a number.");
        }






        public void ShowMainBookMenu()
        {
            Console.WriteLine("Welcome to the Book Menu");

            string choice = "0";
            while (choice != "0")
            {
                choice = AnsiConsole.Prompt(new SelectionPrompt<string>()
               .Title("1. Show Books, 0. Exit")
               .AddChoices("1. Show Books", "0. Exit"));
                switch (choice)
                {
                    case "1. Show Books":
                        ShowMainBookMenu();
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }

    }
}
