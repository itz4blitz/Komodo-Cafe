using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KomodoCafeRepository
{
    public class MenuItemRepository
    {
        private List<MenuItem> _menuItemRepo = new List<MenuItem>();

        public void AddMenuItemToRepo(MenuItem menuItem)
        {
            _menuItemRepo.Add(menuItem);
        }

        public List<MenuItem> GetAllMenuItems()
        {
            return _menuItemRepo;
        }

        public MenuItem GetMenuItemByMealNumber(int mealNumber)
        {
            foreach (MenuItem itemNumber in _menuItemRepo)
            {
                if (itemNumber.MealNumber == mealNumber)
                {
                    return itemNumber;
                }
            }
            return null;
        }

        public void UpdateMenuItem(int menuItemNumber, MenuItem newMenuItem)
        {
            foreach (MenuItem itemNumber in _menuItemRepo)
            {
                if(itemNumber.MealNumber == menuItemNumber)
                {
                    itemNumber.MealNumber = newMenuItem.MealNumber;
                    itemNumber.Name = newMenuItem.Name;
                    itemNumber.Description = newMenuItem.Description;
                    itemNumber.Ingredients = newMenuItem.Ingredients;
                    itemNumber.Price = newMenuItem.Price;
                }
            }
        }

        public void DeleteMenuItem(int menuItemNumber)
        {
            _menuItemRepo.Remove(GetMenuItemByMealNumber(menuItemNumber));
        }
    }
}
