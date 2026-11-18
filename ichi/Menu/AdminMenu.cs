using ichi.Models;
using ichi.Services;

namespace ichi.Menu
{
    internal class AdminMenu
    {
        private readonly BookService _bookService;
        private readonly PurchaseService _orderService;

        public AdminMenu(BookService bookService, PurchaseService orderService)
        {
            _bookService = bookService;
            _orderService = orderService;
        }

        public void Show(User user)
        {
            while (true)
            {
                Console.WriteLine("\n--- Admin menu ---");
                Console.WriteLine("1. Show books");
                Console.WriteLine("2. Add book");
                Console.WriteLine("3. Update book");
                Console.WriteLine("4. Delete book");
                //Console.WriteLine("5. Show all orders");
                Console.WriteLine("0. Logout");
                Console.Write("Choose: ");
                string choice = Console.ReadLine() ?? "";

                try
                {
                    if (choice == "1") ShowBooks();
                    else if (choice == "2") AddBook();
                    else if (choice == "3") UpdateBook();
                    else if (choice == "4") DeleteBook();
                    //else if (choice == "5") ShowOrders();
                    else if (choice == "0") return;
                    else Console.WriteLine("Invalid choice.");
                }
                catch (Exception error)
                {
                    Console.WriteLine("Error: " + error.Message);
                }
            }
        }

        private void ShowBooks()
        {
            List<Book> books = _bookService.GetAllBooks();
            foreach (Book book in books)
                Console.WriteLine($"{book.Id}. {book.Name} - Vol: {book.Vol}. {book.Description}");

            if (books.Count == 0)
                Console.WriteLine("No books found.");
        }

        private void AddBook()
        {
            Console.Write("Book name: ");
            string name = Console.ReadLine() ?? "";
            Console.Write("Book Vol: ");
            string vol = Console.ReadLine() ?? "";
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
