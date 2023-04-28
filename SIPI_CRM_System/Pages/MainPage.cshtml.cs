using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SIPI_CRM_System.Pages
{
	public class MainPageModel : PageModel
    {
        private static string redirectUserString = "";

        public void OnGet()
        {
            redirectUserString = "?login=" + Request.Query["login"] + "&isadmin=" + Request.Query["isadmin"];
        }

        public IActionResult OnPostExit()
        {
            return Redirect("/Index");
        }
    }
}
