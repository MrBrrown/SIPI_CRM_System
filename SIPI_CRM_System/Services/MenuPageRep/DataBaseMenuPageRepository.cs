using System;
using Microsoft.EntityFrameworkCore;
using SIPI_CRM_System.Models;
using SIPI_CRM_System.Services;

namespace SIPI_CRM_System.Services.MenuPageRep
{
	public class DataBaseMenuPageRepository : IMenuPageRepository
	{
        private readonly CRMdbContext _context;

		public DataBaseMenuPageRepository(CRMdbContext context)
		{
            _context = context;
		}

        public IEnumerable<Dish> GetDishes()
        {
            return _context.Dishes.Include(x => x.ProductDishes).ThenInclude(x => x.Product);
        }

        public List<string> GetDishSubCategoryList(string category)
        {
            HashSet<string> dishSubCategories = new();

            foreach (var item in _context.Dishes.Where(x => x.Category == category))
            {
                if (item.SubCategory == null)
                {
                    item.SubCategory = "Уникальные блюда";
                    _context.Dishes.Update(item);
                }
                    dishSubCategories.Add(item.SubCategory);
            }

            var returnList = dishSubCategories.ToList();

            returnList.Sort();

            _context.SaveChanges();

            return returnList;
        }

        public List<string> GetDishCategoryList()
        {
            HashSet<string> dishCategories = new();

            foreach (var item in _context.Dishes)
            {
                dishCategories.Add(item.Category);
            }

            var returnList = dishCategories.ToList();

            returnList.Sort();

            return returnList;
        }

        public async void DeleteDish(int dishId)
        {
            var dish = _context.Dishes.Find(dishId);
            _context.Dishes.Remove(dish);

            await _context.SaveChangesAsync();
        }
    }
}

