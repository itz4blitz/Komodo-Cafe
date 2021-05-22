using KomodoCafeRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KomodoCafe
{
    public class ProgramUI
    {
        private MenuItemRepository _repo = new MenuItemRepository();

        public void Run()
        {
            SeedMenuRepository();
            Menu();
            Console.ReadLine();
        }

        private void Menu()
        {
            Console.Clear();
            Console.WriteLine($"1. Add a menu item" +
                $"\n2. List all menu items" +
                $"\n3. Update a menu item" +
                $"\n4. Delete a menu item");

            string userInput = Console.ReadLine();
            int selectedOption;
            if (int.TryParse(userInput, out selectedOption))
            {
                switch (selectedOption)
                {
                    case 1:
                        AddItemToMenu();
                        break;
                    case 2:
                        DisplayAllMenuItems();
                        break;
                    case 3:
                        UpdateExistingMenuItem();
                        break;
                    case 4:
                        DeleteExistingMenuItem();
                        break;
                    default:
                        Console.Write("Invalid option! Try again: ");
                        break;
                }
            }
            else
            {
                Console.Write("Only numbers are accepted!");
                Thread.Sleep(3000);
                Menu();
            }

        }

        private void AddItemToMenu()
        {
            Console.Clear();
            int itemCount = 0;
            MenuItem newMenuItem = GetValuesForMenuItemObject();
            _repo.AddMenuItemToRepo(newMenuItem);
            if (_repo.ToString().Count() > itemCount)
            {
                Console.Write($"Added #: {newMenuItem.MealNumber}" +
                    $"\nMeal Name: {newMenuItem.Name}" +
                    $"\nDescription: {newMenuItem.Description}" +
                    $"\nIngredients: ");
                foreach (string ingredient in newMenuItem.Ingredients)
                {
                    Console.Write($" {ingredient}");
                }
                Console.Write($"\nPrice: {newMenuItem.Price}\n");

                Console.Write("Press and key to return to the menu");
                Menu();
            }
            else
            {
                Console.WriteLine("There are no items in the menu!");
                Menu();
            }
        }

        private void DisplayAllMenuItems()
        {
            Console.Clear();
            List<MenuItem> menuItems = _repo.GetAllMenuItems();
            foreach (MenuItem menuItem in menuItems)
            {
                Console.WriteLine($"Meal #: {menuItem.MealNumber}" +
                    $"\nMeal Name: {menuItem.Name}" +
                    $"\nDescription: {menuItem.Description}");
                Console.Write($"Ingredients:");
                foreach (string ingredient in menuItem.Ingredients)
                {
                    Console.Write($" {ingredient}");
                }
                Console.WriteLine($"\nPrice: {menuItem.Price}");
                Console.WriteLine();
            }
            Console.WriteLine("Press any key to continue");
            Console.ReadLine();
            Menu();
        }

        private void DisplayMenuItemByNumber()
        {
            Console.Clear();
            DisplayAllMenuItems();
            Console.Write("Enter the # for the menu item to edit: ");
            string userInput = Console.ReadLine();
            int selectedOption;
            if (int.TryParse(userInput, out selectedOption))
            {
                MenuItem meal = _repo.GetMenuItemByMealNumber(selectedOption);
                Console.WriteLine($"Meal #: {meal.MealNumber}" +
                    $"\nName: {meal.Name}" +
                    $"\nDescription: {meal.Description}");
                Console.Write($"Ingredients:");
                foreach (string ingredient in meal.Ingredients)
                {
                    Console.Write($" {ingredient}");
                }
            }
            else
            {
                Console.WriteLine("Invalid selection. Enter a #: ");
            }
        }

        private void UpdateExistingMenuItem()
        {
            Console.Clear();
            Console.WriteLine("Enter the item number of the item you wish to update");
            string userInput = Console.ReadLine();
            int selectedOption;
            if (int.TryParse(userInput, out selectedOption))
            {
                MenuItem menuItem = _repo.GetMenuItemByMealNumber(selectedOption);
                MenuItem newMenuItem = GetValuesForMenuItemObject();
                menuItem.Name = newMenuItem.Name;
                menuItem.Description = newMenuItem.Description;
                menuItem.Ingredients = newMenuItem.Ingredients;
                menuItem.MealNumber = newMenuItem.MealNumber;
                menuItem.Price = newMenuItem.Price;
            }
            else
            {
                Console.WriteLine("Unable to locate that menu item.");
                Thread.Sleep(4000);
                Menu();
            }
        }

        private void DeleteExistingMenuItem()
        {
            Console.Clear();
            Console.Write("Enter the # of the menu item you want to delete: ");
            string userInput = Console.ReadLine();
            int itemNumber;
            int.TryParse(userInput, out itemNumber);

            foreach (MenuItem menuItem in _repo.GetAllMenuItems())
            {
                if (menuItem.MealNumber == itemNumber)
                {
                    _repo.DeleteMenuItem(itemNumber);
                }

                else
                {
                    Console.WriteLine("Unable to locate that menu item.");
                    Thread.Sleep(4000);
                    Menu();
                }
            }
        }

        //Helper method

        private MenuItem GetValuesForMenuItemObject()
        {
            Console.Clear();
            Console.Write("Enter a menu item number: ");
            var menuNumber = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter a menu item name: ");
            string menuName = Console.ReadLine();

            Console.Write("Enter a description: ");
            string description = Console.ReadLine();

            Console.WriteLine("Enter each ingredient and press 'enter'. (Enter 'stop' to finish):");
            bool isRunning = true;
            List<string> ingredientList = new List<string>();

            while (isRunning)
            {
                string ingredient = Console.ReadLine();
                if (ingredient.ToLower() == "stop")
                {
                    isRunning = false;
                    break;
                }
                if(String.IsNullOrWhiteSpace(ingredient) || String.IsNullOrEmpty(ingredient))
                {
                    Console.WriteLine("Ingredient cannot be empty!");
                    break;
                }
                ingredientList.Add(ingredient);
            }

            if (ingredientList.Count <= 0)
            {
                Console.WriteLine("Empty list of ingredients!");
            }

            Console.Write("Enter a price: ");
            string userInput = Console.ReadLine();
            double itemPrice;
            if (double.TryParse(userInput, out itemPrice))
            {

            }

            MenuItem newMenuItem = new MenuItem(menuNumber, menuName, description, ingredientList, itemPrice);
            return newMenuItem;
        }

        public void SeedMenuRepository()
        {
            MenuItem itemOne = new MenuItem(1, "Cheeseburger",
                "An all beef patty, ketchup, cheese, pickle, onion, and bun",
                new List<string> { "beef patty", "cheese", "onion", "pickle", "bun", "ketchup" },
                4.99);

            MenuItem itemTwo = new MenuItem(2, "Chicken Sandwich",
                "A deep-fried chicken breast, mayonaisse, lettuce, pickle, and bun",
                new List<string> { "chicken breast", "mayoinaisse", "lettuce", "pickle", "bun" },
                4.99);

            MenuItem itemThree = new MenuItem(3, "Chicken Strip Basket",
                "3 chicken strips",
                new List<string> { "3  chicken strips" },
                4.99);

            _repo.AddMenuItemToRepo(itemOne);
            _repo.AddMenuItemToRepo(itemTwo);
            _repo.AddMenuItemToRepo(itemThree);
        }
    }
}