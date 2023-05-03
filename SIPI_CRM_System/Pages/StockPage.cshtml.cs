using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SIPI_CRM_System.Models;
using SIPI_CRM_System.Services.ProductRep;

namespace SIPI_CRM_System.Pages;

public class StockPageModel : PageModel
{
    private readonly IProductRepository _repository;
    
    [BindProperty]
    public List<string> CategoryCheck { get; set; }
    
    public IEnumerable<Product> Products { get; set; }

    public List<string> Categories { get; set; }

    public StockPageModel(IProductRepository repository)
    {
        _repository = repository;
    }
    
    public IActionResult OnPostUpdateCategories()
    {
        Products = _repository.GetProductsByCategories(CategoryCheck).OrderBy(x => x.Name);
        Categories = _repository.GetAllCategories();
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

        _repository.Update(product);

        return RedirectToPage();
    }

    public IActionResult OnPostAdd()
    {
        Product product = new Product()
        {
            Id = _repository.GetLastProductId() + 1,
            Name = Request.Form["Name"],
            Amount = int.Parse(Request.Form["Amount"]),
            Category = Request.Form["Category"]
        };

        _repository.AddProduct(product);

        return RedirectToPage();
    }

    public IActionResult OnPostDelete(int id)
    {
        _repository.RemoveProductById(id);
        return RedirectToPage();
    }

    public void OnGet()
    {
        Products = _repository.GetProducts();
        Categories = _repository.GetAllCategories();
    }
}