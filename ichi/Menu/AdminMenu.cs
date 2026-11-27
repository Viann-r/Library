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
                .Title("1. Show books, 2. Add book, 3. Update book, 4. Delete book, 5. Add admin, 6. Shop top 3 clients 0. Exit")
                .AddChoices("1. Show books", "2. Add book", "3. Update book", "4. Delete book", "5. Add admin", "6. Shop top 3 clients", "0. Exit"));
                switch (choice)
                {
                    case "1. Show books":
                        ShowBooks();
                        break;

                    case "2. Add book":
                        AddBook();
                        break;

                    case "3. Update book":
                        UpdateBook();
                        break;

                    case "4. Delete book":
                        DeleteBook();
                        break;

                    case "5. Add admin":
                        AddAdmin();
                        break;

                    case "6. Shop top 3 clients":
                        BestPurchases();
                        break;

                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;

                }
            }
            ;
        }



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
            Console.Write("Book price: ");
            int price = int.Parse(Console.ReadLine() ?? "0");
            Console.Write("How many books in stock: ");
            int instock = int.Parse(Console.ReadLine() ?? "0");
            _bookService.AddBook(name, vol, description, price, instock);
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
            int price = ReadInt("Book price: ");
            int instock = ReadInt("Book instock: ");



            if (_bookService.UpdateBook(id, name, vol, description, price, instock))
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
        public static string BestPurchases()
        {
            var users = _userService.GetAllUsers();
            List<User> topusers = new List<User>();
            int max = 0;
            bool isMax = false;
            bool maxStop = false;
            for (int i = 0; i < users.Count; i++)
            {
                if (topusers.Count == 3)
                {
                    break;
                }
                for (int j = i + 1; j < users.Count; j++)
                {
                    if (users[i].Spent > users[j].Spent)
                    {
                        max = users[i].Spent;
                        isMax = true;
                    }
                    else
                    {
                        break;
                    }
                    if (isMax && j == 10)
                    {
                        users.Remove(users[i]);
                        topusers.Add(users[i]);
                        maxStop = true;
                    }
                }
    ;
                
            }
;
            return $"The best purchases are: {topusers[0].Name}, {topusers[1].Name}, {topusers[2].Name}";
        }
    }
}
