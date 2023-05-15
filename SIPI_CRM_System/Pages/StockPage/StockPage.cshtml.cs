using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SIPI_CRM_System.Models;
using SIPI_CRM_System.Services.StockPage;

namespace SIPI_CRM_System.Pages;

public class StockPageModel : PageModel
{
    private static string redirectUserString = ""; //Необоходимо задавать на каждой станице в методе  OnGet(), по нему происходит сохранение действующего пользователя
    private readonly IStockPageRepository _context;
    
    [BindProperty]
    public List<string> CategoryCheck { get; set; }
    
    public IEnumerable<Product> Products { get; set; }

    public List<string> Categories { get; set; }

    public StockPageModel(IStockPageRepository context)
    {
        _context = context;
    }
    
    public IActionResult OnPostUpdateCategories()
    {
        Products = _context.GetProductsByCategories(CategoryCheck).OrderBy(x => x.Name);
        Categories = _context.GetAllCategories();
        return Page();
    }
    
    public IActionResult OnPostUpdate(int id)
    {
        Product product = new Product()
        {
            Id = id,
            Name = Request.Form["Name"],
            Amount = int.Parse(Request.Form["Amount"]),
            Category = Request.Form["Category"]
        };

        _context.Update(product);

        return Redirect("/StockPage/StockPage" + redirectUserString);
    }

    public IActionResult OnPostAdd()
    {
        Products = _context.GetProducts();

        Product product = new Product()
        {
            Id = Products.Any() ? Products.OrderBy(x => x.Id).Last().Id + 1 : 1,
            Name = Request.Form["Name"],
            Amount = Convert.ToDecimal(Request.Form["Amount"], new CultureInfo("en-US")),
            Category = Request.Form["Category"]
        };

        _context.AddProduct(product);

        return Redirect("/StockPage/StockPage" + redirectUserString);
    }

    public IActionResult OnPostDelete(int id)
    {
        _context.RemoveProductById(id);
        return Redirect("/StockPage/StockPage" + redirectUserString);
    }

    public IActionResult OnPostExit()
    {
        return Redirect("/Index");
    }

    public void OnGet()
    {
        Products = _context.GetProducts();
        Categories = _context.GetAllCategories();

        redirectUserString = "?id=" + Request.Query["id"] + "&isadmin=" + Request.Query["isadmin"]; //Выражение для сохраниения пользователя, одинаково на каждой станице
    }
}