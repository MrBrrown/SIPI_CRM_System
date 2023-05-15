using System;
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
            return _context.Dishes;
        }

        public List<string> GetDishSubCategoryList(string category)
        {
            HashSet<string> dishSubCategories = new();

            foreach (var item in _context.Dishes.Where(x => x.Category == category))
            {
                if (item.SubCategory == null)
                {
                    continue;
                }

                dishSubCategories.Add(item.SubCategory);
            }

            var returnList = dishSubCategories.ToList();

            returnList.Sort();

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
    }
}

