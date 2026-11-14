using ichi.Services;
using Spectre.Console;

namespace ichi.Menu
{
    internal class ClientMenu
    {
        private readonly BookService _bookService;
        public ClientMenu(BookService bookService)
        {
            _bookService = bookService;
        }
        public void ShowMainFoodMenu()
        {
            Console.WriteLine("Welcome to the Food Menu");

            string choice = "0";
            while (choice != "0")
            {
                choice = AnsiConsole.Prompt(new SelectionPrompt<string>()
               .Title("1. Show Foods, 0. Exit")
               .AddChoices("1. Show Foods", "0. Exit"));
                switch (choice)
                {
                    case "1. Show Foods":
                        ShowMainFoodMenu();
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }

    }
}
