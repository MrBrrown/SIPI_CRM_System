using System;
using SIPI_CRM_System.Models;

namespace SIPI_CRM_System.Services.MenuPageRep
{
	public interface IMenuPageRepository
	{
        IEnumerable<Dish> GetDishes();

        List<string> GetDishCategoryList();

		List<string> GetDishSubCategoryList(string category);

		void DeleteDish(int dishId);

		void AddDish(Dish dish);
	}
}

