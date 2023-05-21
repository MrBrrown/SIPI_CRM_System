using System;
using Microsoft.EntityFrameworkCore;
using SIPI_CRM_System.Models;

namespace SIPI_CRM_System.Services.DishEditPageRep
{
	public class DataBaseDishEditPageRepository : IDishEditPageRepository
	{
		private readonly CRMdbContext _context;

		public DataBaseDishEditPageRepository(CRMdbContext context)
		{
			_context = context;
		}

        public async void AddProductDish(ProductDish productDish)
        {
            _context.ProductDishes.Add(productDish);
            await _context.SaveChangesAsync();
        }

        public void DeleteDish(Dish dish)
        {
            throw new NotImplementedException();
        }

        public Dish GetDish(int id)
        {
            return _context.Dishes.Include(x => x.ProductDishes).FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<ProductDish> GetProductDishes()
        {
            return _context.ProductDishes;
        }

        public IEnumerable<Product> GetProducts()
        {
            return _context.Products;
        }

        public List<string> GetProductsCategoriesList()
        {
            HashSet<String> productsCategories = new();

            foreach (var product in _context.Products)
            {
                productsCategories.Add(product.Category);
            }

            var productCategoriesList = productsCategories.ToList();

            productCategoriesList.Sort();

            return productCategoriesList;
        }

        public async void UpdateDish(Dish dish)
        {
            _context.Dishes.Update(dish);
            await _context.SaveChangesAsync();
        }

        public async void UpdateProductDishes(int productDishesId, decimal amount)
        {
            var productDish = _context.ProductDishes.Find(productDishesId);

            productDish.Amount = amount;

            _context.ProductDishes.Update(productDish);

            await _context.SaveChangesAsync();
        }
    }
}

