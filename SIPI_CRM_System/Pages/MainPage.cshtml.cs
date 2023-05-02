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

        public MainPageModel(IMainPageRepository context)
        {
            _context = context;
        }

        public IActionResult OnPostCreate()
        {
            var date = Request.Form["Date"];
            var time = Request.Form["Time"];

            if (time == "")
                _context.CreateDailyOrder(DateTime.Now, false);
            else
                _context.CreateDailyOrder(DateTime.Parse(date + " " + time), true);

            return Redirect("/MainPage" + redirectUserString);
        }

        public void OnGet()
        {
            dailyOrders = _context.GetDayliOrders().Where(x => x.OrderDateTime.ToShortDateString() == DateTime.Now.ToShortDateString())
                .OrderByDescending(x => x.IsReserved).
                ThenByDescending(x => x.OrderDateTime);
            redirectUserString = "?login=" + Request.Query["login"] + "&isadmin=" + Request.Query["isadmin"]; //Выражение для сохраниения пользователя, одинаково на каждой станице
        }

        public IActionResult OnPostExit()
        {
            return Redirect("/Index");
        }
    }
}
