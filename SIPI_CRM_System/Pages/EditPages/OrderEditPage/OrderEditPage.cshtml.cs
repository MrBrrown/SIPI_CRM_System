using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SIPI_CRM_System.Models;
using SIPI_CRM_System.Services.MenuPageRep;
using SIPI_CRM_System.Services.OrderEditPageRep;

namespace SIPI_CRM_System.Pages.EditPages.OrderEditPage
{
	public class OrderEditPageModel : PageModel
    {
        private readonly IOrderEditPageRepository _context;
        private static string redirectUserString = ""; //Необоходимо задавать на каждой станице в методе  OnGet(), по нему происходит сохранение действующего пользователя

        public DailyOrder dailyOrder;

        public IEnumerable<DailyOrderDish> dailyOrderDishes;
        public IEnumerable<Dish> dishes;

        public List<string> dishCategories = new();
        public Dictionary<string, List<string>> dishSubCategories = new();

        public OrderEditPageModel(IOrderEditPageRepository context)
        {
            _context = context;
        }

        public IActionResult OnPostExit()
        {
            return Redirect("/MainPage/MainPage" + redirectUserString);
        }

        public IActionResult OnPostAddDishToOrder(int dishId, int orderId)
        {
            dailyOrderDishes = _context.GetDailyOrderDishes();

            if (dailyOrderDishes
                .Where(x => x.DishId == dishId)
                .Where(x => x.DailyOrderId == orderId)
                .FirstOrDefault() == default)
            {
                var dailyOrderDish = new DailyOrderDish()
                {
                    Id = _context.GetDailyOrderDishes().Any() ? _context.GetDailyOrderDishes().Last().Id + 1 : 1,
                    DishId = dishId,
                    DailyOrderId = orderId,
                    Amount = 0
                };

                _context.AddDailyOrderDish(dailyOrderDish);
            }

            return Redirect("/EditPages/OrderEditPage/OrderEditPage" + redirectUserString + "&orderId=" + orderId.ToString());
        }

        public IActionResult OnPostDeleteOrderDish(int dailyOrderDishId, int orderId)
        {
            _context.DeleteDailyOrderDish(dailyOrderDishId);
            return Redirect("/EditPages/OrderEditPage/OrderEditPage" + redirectUserString + "&orderId=" + orderId.ToString());
        }

        public IActionResult OnPostUpdate()
        {
            return Redirect("");
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

            dailyOrderDishes = _context.GetDailyOrderDishes();

            dailyOrder = _context.GetDailyOrder(int.Parse(Request.Query["orderId"]));

            redirectUserString = "?id=" + Request.Query["id"] + "&isadmin=" + Request.Query["isadmin"]; //Выражение для сохраниения пользователя, одинаково на каждой станице
        }
    }
}
