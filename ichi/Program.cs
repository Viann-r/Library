using ichi.Menu;
using ichi.Repositories;
using ichi.Services;

namespace ichi
{
    internal class Program
    {
        static void Main(string[] args)
        {
            UserRepository repository = new UserRepository();
            UserService userService = new UserService(repository);
            UserMenu userMenu = new UserMenu(userService);

            userMenu.ShowMainMenu();
        }
    }
}
