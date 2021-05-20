using KomodoCafeRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KomodoCafe
{
    public class ProgramUI
    {
        //POCO

        private MenuItemRepository _repo = new MenuItemRepository();

        public void Run()
        {
            SeedMenuRepository();
            Menu();
            Console.ReadLine();
        }

        private void Menu()
        {
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
                        CreateNewMenuItem();
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
            }

        }

        private bool CreateNewMenuItem()
        {
            int itemCount = 0;
            MenuItem newItem = GetValuesForMenuItemObject();
            _repo.AddMenuItemToRepo(newItem);
            if (_repo.ToString().Count() > itemCount)
            {
                Console.WriteLine($"Added #: {newItem.MealNumber}" +
                    $"\n Meal Name: {newItem.Name}" +
                    $"\n Description: {newItem.Description}" +
                    $"\n Ingredients: {newItem.Ingredients}" +
                    $"\n Price: {newItem.Price}");
                return true;
            }
            else
            {
                Console.WriteLine("There are no items in the menu!");
                Menu();
                return false;
            }
        }

        private void DisplayAllMenuItems()
        {
            Console.Clear();
            List<MenuItem> menuItems = _repo.GetAllMenuItems();
            foreach (MenuItem item in menuItems)
            {
                Console.WriteLine($"Meal #: {item.MealNumber}" +
                    $"\nMeal Name: {item.Name}" +
                    $"\nDescription: {item.Description}");
                Console.Write($"Ingredients:");
                foreach (string ingredient in item.Ingredients)
                {
                    Console.Write($" {ingredient}");
                }
                Console.WriteLine($"\nPrice: {item.Price}");
                Console.WriteLine();
            }
        }

        private void DisplayMenuItemByNumber()
        {
            Console.Clear();
            DisplayAllMenuItems();
            Console.WriteLine("Enter the # for the menu item you wish to edit: ");
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

        }

        private void DeleteExistingMenuItem()
        {

        }

        //Helper method

        private MenuItem GetValuesForMenuItemObject()
        {
            Console.Clear();
            Console.Write("Enter a menu item number: ");
            var itemNumber = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter a menu item name: ");
            string itemName = Console.ReadLine();

            Console.Write("Enter a description: ");
            string description = Console.ReadLine();

            Console.WriteLine("Provide a list of ingredients (enter 'stop' to finish:");
            bool isTrue = true;
            List<string> ingredientList = new List<string>();
            while (isTrue)
            {
                string ingredient = Console.ReadLine();
                if (ingredient.ToLower() == "stop")
                {
                    isTrue = false;
                }
                ingredientList.Add(ingredient);
            }
            if (ingredientList.Count > 0)
            {
                foreach (string ingredient in ingredientList)
                {
                    Console.WriteLine($" {ingredient}");
                }
            }

            Console.Write("Enter a price: ");
            double itemPrice = Convert.ToDouble(Console.ReadLine());

            MenuItem newMenuItem = new MenuItem(itemNumber, itemName, description, ingredientList, itemPrice);
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