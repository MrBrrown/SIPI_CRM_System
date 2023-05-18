using System;
using SIPI_CRM_System.Models;

namespace SIPI_CRM_System.Services.DishEditPageRep
{
	public interface IDishEditPageRepository
	{
		Dish GetDish(int id);

		IEnumerable<Product> GetProducts();

        List<String> GetProductsCategoriesList();

        void UpdateDish(Dish dish);

		void DeleteDish(Dish dish);
	}
}

