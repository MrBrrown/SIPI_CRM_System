using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SIPI_CRM_System.Models;
using SIPI_CRM_System.Services.MainPageRep;

namespace SIPI_CRM_System.Pages
{
	public class MainPageModel : PageModel
    {
        private static string redirectUserString = ""; //Необоходимо задавать на каждой станице в методе  OnGet(), по нему происходит сохранение действующего пользователя
        private readonly IMainPageRepository _context;

        public IEnumerable<DailyOrder> dailyOrders;
        public IEnumerable<Dish> dishes;
        public IEnumerable<Table> tables;

        public MainPageModel(IMainPageRepository context)
        {
            _context = context;
        }

        public IActionResult OnPostCreate()
        {
            var date = Request.Form["Date"];
            var time = Request.Form["Time"];
            var tableId = Request.Form["TableId"];
            dailyOrders = _context.GetDayliOrders();

            var order = new DailyOrder()
            {
                Id = dailyOrders.Any() ? dailyOrders.OrderBy(x => x.Id).Last().Id + 1 : 1,
                OrderDateTime = time == "" ? DateTime.Now : DateTime.Parse(date + " " + time),
                IsReserved = time == "" ? false : true,
                IsDone = false,
                TableId = Int32.Parse(tableId)
            };

            _context.CreateDailyOrder(order);

            return Redirect("/MainPage/MainPage" + redirectUserString);
        }

        public IActionResult OnPostSetOrderDone(int id)
        {
            _context.SetOrderStatusDone(id);

            _context.RemoveProducts(id);

            return Redirect("/MainPage/MainPage" + redirectUserString);
        }

        public IActionResult OnPostDeleteOrder(int id)
        {
            _context.DeleteDailyOrder(id);
            return Redirect("/MainPage/MainPage" + redirectUserString);
        }

        public IActionResult OnPostSetReservedOrderActive(int id)
        {
            var order = _context.GetDailyOrderById(id);

            order.IsReserved = false;

            _context.UpdateDailyOrder(order);

            return Redirect("/MainPage/MainPage" + redirectUserString);
        }

        public IActionResult OnPostExit()
        {
            return Redirect("/Index");
        }

        public IActionResult OnPostCloseWorkDay(int id)
        {
            if (_context.CheckPassword(id, Request.Form["Password"]))
            {
                _context.CloseWorkDay();
                return Redirect("/MainPage/MainPage" + redirectUserString);
            }
            else
            {
                return Redirect("/MainPage/MainPage" + redirectUserString + "&invalidpassword=True");
            }
        }

        public void OnGet()
        {
            dailyOrders = _context.GetDayliOrders()
                .OrderByDescending(x => x.IsReserved)
                .ThenBy(x => x.IsDone)
                .ThenByDescending(x => x.OrderDateTime);

            dishes = _context.GetDishes();

            tables = _context.GetTables();

            redirectUserString = "?id=" + Request.Query["id"] + "&isadmin=" + Request.Query["isadmin"]; //Выражение для сохраниения пользователя, одинаково на каждой станице
        }
    }
}
