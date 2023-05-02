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
        private static string redirectUserString = "";
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
            DateTime dateTime = DateTime.Parse(date + " " + time);

            _context.CreateDailyOrder(dateTime, true);

            return Redirect("/MainPage" + redirectUserString);
        }

        public void OnGet()
        {
            dailyOrders = _context.GetDayliOrders();
            redirectUserString = "?login=" + Request.Query["login"] + "&isadmin=" + Request.Query["isadmin"];
        }

        public IActionResult OnPostExit()
        {
            return Redirect("/Index");
        }
    }
}
