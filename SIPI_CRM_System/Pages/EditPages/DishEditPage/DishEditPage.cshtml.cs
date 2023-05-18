using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SIPI_CRM_System.Models;
using SIPI_CRM_System.Services.DishEditPageRep;

namespace SIPI_CRM_System.Pages.EditPages.DishEditPage
{
	public class DishEditPageModel : PageModel
    {
        private readonly IDishEditPageRepository _context;

        public List<String> productsCategories;

        public IEnumerable<Product> products;

        public DishEditPageModel(IDishEditPageRepository context)
        {
            _context = context;
        }

        public void OnPostTest()
        {

        }


        public void OnGet()
        {
            productsCategories = _context.GetProductsCategoriesList();
            products = _context.GetProducts();
        }
    }
}
