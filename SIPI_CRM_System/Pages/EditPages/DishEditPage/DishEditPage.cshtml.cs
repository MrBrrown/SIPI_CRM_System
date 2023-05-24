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
        private static string redirectUserString = ""; //Необоходимо задавать на каждой станице в методе  OnGet(), по нему происходит сохранение действующего пользователя

        private readonly IDishEditPageRepository _context;

        public Dish dish;

        public List<String> productsCategories;

        public IEnumerable<Product> products;

        public IEnumerable<ProductDish> productDishes;

        public DishEditPageModel(IDishEditPageRepository context)
        {
            _context = context;
        }

        public IActionResult OnPostAddProductDish(int productId, int dishId)
        {
            productDishes = _context.GetProductDishes();
            if (productDishes
                .Where(x => x.DishId == dishId)
                .Where(x => x.ProductId == productId)
                .FirstOrDefault() == default)
            {
                var productDish = new ProductDish
                {
                    Id = _context.GetProductDishes().Any() ? _context.GetProductDishes().Last().Id + 1 : 1,
                    ProductId = productId,
                    DishId = dishId,
                    Amount = 0
                };

                _context.AddProductDish(productDish);
            }

            return Redirect("/EditPages/DishEditPage/DishEditPage" + redirectUserString + "&dishId=" + dishId.ToString());
        }

        public IActionResult OnPostUpdate(int dishId)
        {
            var dishName = Request.Form["DishName"];
            var dishMass = decimal.Parse(Request.Form["DishMass"]);

            var updDish = _context.GetDish(dishId);

            if (dishName != updDish.Name || dishMass != updDish.Mass)
            {
                updDish.Name = dishName;
                updDish.Mass = dishMass;
                _context.UpdateDish(updDish);
            }

            foreach (var updProductDish in updDish.ProductDishes)
            {
                var amount = decimal.Parse(Request.Form["productDishesAmount" + updProductDish.Id.ToString()]);

                _context.UpdateProductDishes(updProductDish.Id, amount);
            }

            return Redirect("/EditPages/DishEditPage/DishEditPage" + redirectUserString + "&dishId=" + dishId.ToString());
        }

        public IActionResult OnPostExit()
        {
            return Redirect("/MenuPage/MenuPage" + redirectUserString);
        }

        public IActionResult OnPostDeleteProductDish(int dishId,int productDishId)
        {
            _context.DeleteProductDish(productDishId);
            return Redirect("/EditPages/DishEditPage/DishEditPage" + redirectUserString + "&dishId=" + dishId.ToString());
        }

        public void OnGet()
        {
            dish = _context.GetDish(int.Parse(Request.Query["dishId"]));

            productsCategories = _context.GetProductsCategoriesList();
            products = _context.GetProducts().OrderBy(x => x.Name);
            productDishes = _context.GetProductDishes();

            redirectUserString = "?id=" + Request.Query["id"] + "&isadmin=" + Request.Query["isadmin"]; //Выражение для сохраниения пользователя, одинаково на каждой станице
        }
    }
}
