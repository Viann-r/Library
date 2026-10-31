using ichi.Models;
using ichi.Services;

namespace ichi.Menu
{
    internal class Menu
    {
        private readonly UserService _userService;
        public Menu(UserService userService)
        {
            _userService = userService;
        }

        public void ShowMenu()
        {

            Console.WriteLine("Welcome to user Management Systeme");
            Console.WriteLine("1. Register");
            Console.WriteLine("2. Login");
            Console.WriteLine("0. Exit");
            var choice = Console.ReadLine();
            while (choice != "0")
            {
                switch (choice)
                {
                    case "1":
                        ShowRegisterMenu();
                        break;

                    case "2":
                        ShowLoginMenu();
                        break;

                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;

                }
            }
        }
        private void ShowRegisterMenu()
        {
            User user = new User();
            Console.WriteLine("Enter username: ");
            user.Name = Console.ReadLine();
            Console.WriteLine("Enter email: ");
            user.Email = Console.ReadLine();
            Console.WriteLine("Enter password: ");
            user.Password = Console.ReadLine();
        }
        private void ShowLoginMenu()
        {
            User user = new User();
            Console.WriteLine("Enter email: ");
            user.Email = Console.ReadLine();
            Console.WriteLine("Enter password: ");
            user.Password = Console.ReadLine();
        }
    }
}
