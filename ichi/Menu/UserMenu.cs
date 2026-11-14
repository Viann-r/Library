using ichi.Models;
using ichi.Services;
using Spectre.Console;

namespace ichi.Menu
{
    internal class UserMenu
    {
        private readonly UserService _userService;
        private readonly ClientMenu _clientMenu;
        public UserMenu(UserService userService)
        {
            _userService = userService;
        }

        public void ShowMainMenu()
        {
            Console.WriteLine("Welcome to the user managment system!");

            string choice = "";
            while (choice != "0")
            {
                //Console.WriteLine("1. Register");
                //Console.WriteLine("2. Login");
                //Console.WriteLine("0. Exit");

                choice = AnsiConsole.Prompt(new SelectionPrompt<string>()
                .Title("1. Register, 2. Login, 3. Verify Email, 0. Exit")
                .AddChoices("1. Register", "2. Login", "3. Verify Email", "0. Exit"));
                //choice = Console.ReadLine();
                switch (choice)
                {
                    case "1. Register":
                        ShowRegisterMenu();
                        break;

                    case "2. Login":
                        ShowLoginMenu();
                        break;

                    case "3. Verify Email":
                        ShowVerifyEmailMenu();
                        break;

                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;

                }
            }
        }

        private void ShowVerifyEmailMenu()
        {
            Console.WriteLine("Enter email:");
            string email = Console.ReadLine();
            Console.WriteLine("Enter verification code:");
            string code = Console.ReadLine();
            _userService.VerifyUser(email, code);
        }



        private void ShowRegisterMenu()
        {
            User user = new User();
            Console.WriteLine("Enter username: ");
            user.Name = Console.ReadLine();
            Console.WriteLine("Enter email: ");
            user.Email = Console.ReadLine();
            Console.WriteLine("Enter password: ");
            //user.Password = Console.ReadLine();
            //_userService.RegisterUser(user);
            var password = new TextPrompt<string>("Enter your [green]password[/]:").Secret();

            var result = AnsiConsole.Prompt(password);

            //AnsiConsole.MarkupLine($"Password length: [blue]{result.Length}[/] characters");

            user.Password = result;
            _userService.RegisterUser(user);
        }

        private void ShowLoginMenu()
        {
            User user = new User();
            Console.WriteLine("Enter email: ");
            user.Email = Console.ReadLine();
            Console.WriteLine("Enter password: ");
            user.Password = Console.ReadLine();
            _userService.LoginUser(user.Email, user.Password);
            _clientMenu.ShowMainFoodMenu();
        }



    }
}
