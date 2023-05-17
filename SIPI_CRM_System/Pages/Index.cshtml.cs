using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SIPI_CRM_System.Services.LoginPageRep;

namespace SIPI_CRM_System.Pages;

public class IndexModel : PageModel
{
    private readonly ILoginPageRepository _context;

    public IndexModel(ILoginPageRepository context)
    {
        _context = context;
    }

    public IActionResult OnPost()
    {
        var Login = Request.Form["Login"];
        var Password = Request.Form["Password"];

        if (_context.CheckAuthorization(Login, Password))
        {
            var employee = _context.GetEmployee(Login);
            return Redirect("/StockPage/StockPage?login=" + Login+"&isadmin="+employee.IsAdmin.ToString());
            
            return Redirect("/MainPage/MainPage?login=" + Login+"&isadmin="+employee.IsAdmin.ToString());
        }
        else
            return Redirect("/Index?invalid=True");
    }
}

