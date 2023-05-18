using System;
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

        public void DeleteDish(Dish dish)
        {
            throw new NotImplementedException();
        }

        public Dish GetDish(int id)
        {
            throw new NotImplementedException();
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

        public void UpdateDish(Dish dish)
        {
            throw new NotImplementedException();
        }
    }
}

