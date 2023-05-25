using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SIPI_CRM_System.Models;
using SIPI_CRM_System.Services.MenuPageRep;

namespace SIPI_CRM_System.Pages.MenuPage
{
	public class MenuPageModel : PageModel
    {
        private static string redirectUserString = ""; //Необоходимо задавать на каждой станице в методе  OnGet(), по нему происходит сохранение действующего пользователя
        private readonly IMenuPageRepository _context;

        public IEnumerable<Dish> dishes;
        public List<string> dishCategories = new();
        public Dictionary<string, List<string>> dishSubCategories = new();

        public MenuPageModel(IMenuPageRepository context)
        {
            _context = context;
        }

        public IActionResult OnPostExit()
        {
            return Redirect("/Index");
        }

        public IActionResult OnPostDelete(int dishId)
        {
            _context.DeleteDish(dishId);
            return Redirect("/MenuPage/MenuPage" + redirectUserString);
        }

        public IActionResult OnPostAddDish()
        {
            var dish = new Dish()
            {
                Id = _context.GetDishes().Any() ? _context.GetDishes().OrderBy(x => x.Id).Last().Id + 1 : 1,
                Name = Request.Form["Name"],
                Category = Request.Form["Category"],
                SubCategory = Request.Form["SubCategory"],
                Mass = decimal.Parse(Request.Form["Mass"]),
                Price = decimal.Parse(Request.Form["Price"])
            };

            _context.AddDish(dish);

            return Redirect("/MenuPage/MenuPage" + redirectUserString);
        }

        public void OnGet()
        {
            dishCategories = _context.GetDishCategoryList();

            foreach (var item in dishCategories)
            {
                var subCategories = _context.GetDishSubCategoryList(item); 

                dishSubCategories.Add(item, subCategories);
            }

            dishes = _context.GetDishes();

            redirectUserString = "?id=" + Request.Query["id"] + "&isadmin=" + Request.Query["isadmin"]; //Выражение для сохраниения пользователя, одинаково на каждой станице
        }
    }
}
