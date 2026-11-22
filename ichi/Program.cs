using ichi.Menu;
using ichi.Repositories;
using ichi.Services;
using Microsoft.Extensions.Configuration;

namespace ichi
{
    internal class Program
    {
        static void Main(string[] args)
        {
			
			IConfiguration configuration = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

            UserRepository userRepository = new UserRepository();
            BookRepository bookRepository = new BookRepository();
            PurchaseRepository purchaseRepository = new PurchaseRepository();

            EmailService emailService = new EmailService(configuration);
  
            BookService bookService = new BookService(bookRepository);
            PurchaseService purchaseService = new PurchaseService(purchaseRepository);

			ClientMenu clientMenu = new ClientMenu(bookService, purchaseService);

			UserService userService = new UserService(userRepository, emailService, clientMenu );

			AdminMenu adminMenu = new AdminMenu(bookService, userService, purchaseService);
			UserMenu userMenu = new UserMenu(configuration, userService, clientMenu, adminMenu);

			userMenu.ShowMainMenu();
        }
    }
}
