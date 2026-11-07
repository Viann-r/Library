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
            EmailService emailService = new EmailService(configuration);
            UserService userService = new UserService(userRepository, emailService);
            UserMenu userMenu = new UserMenu(userService);

            userMenu.ShowMainMenu();
        }
    }
}
