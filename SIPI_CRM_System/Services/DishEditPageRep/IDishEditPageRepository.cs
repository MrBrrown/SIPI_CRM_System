using System;
using SIPI_CRM_System.Models;

namespace SIPI_CRM_System.Services.DishEditPageRep
{
	public interface IDishEditPageRepository
	{
		Dish GetDish(int id);

		IEnumerable<Product> GetProducts();

		IEnumerable<ProductDish> GetProductDishes();

        List<String> GetProductsCategoriesList();

        void UpdateDish(Dish dish);

        void UpdateProductDishes(int productDishesId, decimal amount);

        void DeleteProductDish(int id);

		void AddProductDish(ProductDish productDish);
	}
}

