using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using OfficeOpenXml;
using OfficeOpenXml.FormulaParsing.Exceptions;
using SIPI_CRM_System.Models;
using SIPI_CRM_System.Pagination;
using SIPI_CRM_System.Services.StockPage;

namespace SIPI_CRM_System.Pages;

public class StockPageModel : PageModel
{
    private static string redirectUserString = ""; //Необоходимо задавать на каждой станице в методе  OnGet(), по нему происходит сохранение действующего пользователя
    private static string fileLoadException = "";
    private readonly IStockPageRepository _repository;

    [BindProperty(SupportsGet = true)] public List<string> CategoryCheck { get; set; }
    [BindProperty] public string? DeliveryDateSortLabel { get; set; } = "■";
    [BindProperty] public string FitnessSortLabel { get; set; } = "■";
    [BindProperty] public string NameSortLabel { get; set; } = "■";
    
    [BindProperty] public IFormFile FormFile { get; set; }
    
    public List<string> SelectedCategories { get; set; }
    public PaginatedList<Product> PaginatedProducts { get; set; }
    public IEnumerable<Product> Products { get; set; }

    public List<string> Categories { get; set; }

    private int _pageSize;

    public StockPageModel(IStockPageRepository repository)
    {
        _repository = repository;
        _pageSize = 3;
    }
    
    public IActionResult OnPostUpdateCategories()
    {
        Response.Cookies.Append("CategoryCheck", JsonConvert.SerializeObject(CategoryCheck));
        PaginatedProducts = PaginatedList<Product>.Create(
            _repository.GetProductsByCategories(CategoryCheck).ToList(), 1, _pageSize);
        Categories = _repository.GetAllCategories();
        SelectedCategories = Categories.Where(c => CategoryCheck.Contains(c)).ToList();
        SelectedCategories.Add("Fictitious category");
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

        return Redirect("/StockPage/StockPage" + redirectUserString);
    }

    public IActionResult OnPostAdd()
    {
        Products = _repository.GetProducts();

        try
        {
            Product product = new Product()
            {
                Id = Products.Any() ? Products.OrderBy(x => x.Id).Last().Id + 1 : 1,
                Name = Request.Form["Name"],
                Amount = int.Parse(Request.Form["Amount"]),
                Category = Request.Form["Category"],
                LifeTime = int.Parse(Request.Form["LifeTime"]),
                DeliveryDateTime = DateTime.Parse(Request.Form["DeliveryDateTime"])
            };

            _repository.AddProduct(product);

            return Redirect("/StockPage/StockPage" + redirectUserString);
        }
        catch (Exception e)
        {
            return Redirect("/StockPage/StockPage" + redirectUserString + "&incorrectFormat=True");
        }
    }

    public IActionResult OnPostResetCategories()
    {
        Categories = _repository.GetAllCategories();
        PaginatedProducts = PaginatedList<Product>.Create(
            _repository.GetProducts().ToList(), 1, _pageSize);
        SelectedCategories = new List<string> {
            // shit shit shit shit happens
            "Fictitious category" };

        var emptyList = new List<string>(); 
        Response.Cookies.Delete("CategoryCheck");
        Response.Cookies.Append("CategoryCheck", JsonConvert.SerializeObject(emptyList));
        return Page();
    }
    
    public IActionResult OnPostResetSorting()
    {
        DeliveryDateSortLabel = "■";
        FitnessSortLabel = "■";
        NameSortLabel = "■";

        return Redirect("/StockPage/StockPage" + redirectUserString);
    }
    
    public async Task<IActionResult> OnPostAddByDocument(IFormFile formFile)
    {
        try
        {
            if (formFile == null)
            {
                fileLoadException = "&loadException=True";
                throw new NullReferenceException();
            }
            if (!Path.GetExtension(formFile.FileName).Equals(".xlsx", StringComparison.OrdinalIgnoreCase))
            {
                fileLoadException = "&incorrectExtension=True";
                throw new NullReferenceException();
            }  
        }
        catch (Exception e)
        {
            return Redirect("/StockPage/StockPage" + redirectUserString + fileLoadException);
        }

        Products = _repository.GetProducts();

        try
        {
            using (var stream = new MemoryStream())
            {
                await formFile.CopyToAsync(stream);
            
                using (var package = new ExcelPackage(stream))
                {
                    var worksheet = package.Workbook.Worksheets[0];
                    var start = worksheet.Dimension.Start;
                    var end = worksheet.Dimension.End;

                    int idInc = PaginatedProducts.OrderBy(x => x.Id).Last().Id;
                
                    for (int row = start.Row + 1; row <= end.Row; row++)
                    {
                        var product = new Product
                        {
                            Id = PaginatedProducts.Any() ? ++idInc : 1,
                            Name = worksheet.Cells[row, 1].GetValue<string>(),
                            Category = worksheet.Cells[row, 2].GetValue<string>(),
                            Amount = worksheet.Cells[row, 3].GetValue<int>(),
                            LifeTime = worksheet.Cells[row, 4].GetValue<int>(),
                            DeliveryDateTime = worksheet.Cells[row, 5].GetValue<DateTime>()
                        };

                        if(string.IsNullOrEmpty(product.Name) || string.IsNullOrEmpty(product.Category)
                                                              || product.Amount == default || product.LifeTime == default 
                                                              || product.DeliveryDateTime < DateTime.Now)
                        {
                            throw new ArgumentException("One or more product properties have invalid values.");
                        }
                        
                        _repository.AddProduct(product);
                    }
                }
            }
        }
        catch (Exception e)
        {
            fileLoadException = "&fileProcessingException=True";
            return Redirect("/StockPage/StockPage" + redirectUserString + fileLoadException);
        }

        return Redirect("/StockPage/StockPage" + redirectUserString);
    }

    public IActionResult OnPostDelete(int id)
    {
        _repository.RemoveProductById(id);
        return Redirect("/StockPage/StockPage" + redirectUserString);
    }

    public IActionResult OnPostExit()
    {
        return Redirect("/Index");
    }

    public IActionResult OnPostUpdateProductsOrderByDeliveryDate(string deliveryDateSortLabel)
    {
        var categoryCheckCookie = Request.Cookies["CategoryCheck"];
        
        CategoryCheck = string.IsNullOrEmpty(categoryCheckCookie)
            ? new List<string>()
            : JsonConvert.DeserializeObject<List<string>>(categoryCheckCookie);
        Categories = _repository.GetAllCategories();
        SelectedCategories = Categories.Where(c => CategoryCheck.Contains(c)).ToList();
        SelectedCategories.Add("Fictitious category");
        
        switch (deliveryDateSortLabel)
        {
            case "■":
                DeliveryDateSortLabel = "▼";
                PaginatedProducts = PaginatedList<Product>.Create(
                    _repository.GetProductsByCategories(CategoryCheck).OrderByDescending(x => x.DeliveryDateTime).ToList());
                break;
            case "▼":
                DeliveryDateSortLabel = "▲";
                PaginatedProducts = PaginatedList<Product>.Create(
                    _repository.GetProductsByCategories(CategoryCheck).OrderBy(x => x.DeliveryDateTime).ToList());
                break;
            case "▲":
                DeliveryDateSortLabel = "■";
                PaginatedProducts = PaginatedList<Product>.Create(
                    _repository.GetProductsByCategories(CategoryCheck).ToList());
                break;
        }

        return Page();
    }
    
    public IActionResult OnPostUpdateProductsOrderByName(string nameSortLabel)
    {
        var categoryCheckCookie = Request.Cookies["CategoryCheck"];
        
        CategoryCheck = string.IsNullOrEmpty(categoryCheckCookie)
            ? new List<string>()
            : JsonConvert.DeserializeObject<List<string>>(categoryCheckCookie);
        Categories = _repository.GetAllCategories();
        SelectedCategories = Categories.Where(c => CategoryCheck.Contains(c)).ToList();
        SelectedCategories.Add("Fictitious category");
        
        switch (nameSortLabel)
        {
            case "■":
                NameSortLabel = "▼";
                PaginatedProducts = PaginatedList<Product>.Create(
                    _repository.GetProductsByCategories(CategoryCheck).OrderByDescending(x => x.Name).ToList());
                break;
            case "▼":
                NameSortLabel = "▲";
                PaginatedProducts = PaginatedList<Product>.Create(
                    _repository.GetProductsByCategories(CategoryCheck).OrderBy(x => x.Name).ToList());
                break;
            case "▲":
                NameSortLabel = "■";
                PaginatedProducts = PaginatedList<Product>.Create(
                    _repository.GetProductsByCategories(CategoryCheck).ToList());
                break;
        }

        return Page();
    }
    
    public IActionResult OnPostUpdateProductsOrderByFitness(string fitnessSortLabel)
    {
        var categoryCheckCookie = Request.Cookies["CategoryCheck"];
        
        CategoryCheck = string.IsNullOrEmpty(categoryCheckCookie)
            ? new List<string>()
            : JsonConvert.DeserializeObject<List<string>>(categoryCheckCookie);
        Categories = _repository.GetAllCategories();
        SelectedCategories = Categories.Where(c => CategoryCheck.Contains(c)).ToList();
        SelectedCategories.Add("Fictitious category");

        switch (fitnessSortLabel)
        {
            case "■":
                FitnessSortLabel = "▼";
                PaginatedProducts = PaginatedList<Product>.Create(
                    _repository.GetProductsByCategories(CategoryCheck).OrderByDescending(x => x.DeliveryDateTime.AddHours(x.LifeTime) - DateTime.Now).ToList());
                break;
            case "▼":
                FitnessSortLabel = "▲";
                PaginatedProducts = PaginatedList<Product>.Create(
                    _repository.GetProductsByCategories(CategoryCheck).OrderBy(x => x.DeliveryDateTime.AddHours(x.LifeTime) - DateTime.Now).ToList());
                break;
            case "▲":
                FitnessSortLabel = "■";
                PaginatedProducts = PaginatedList<Product>.Create(
                    _repository.GetProductsByCategories(CategoryCheck).ToList());
                break;
        }

        return Page();
    }

    public void OnGet(int? pageIndex)
    {
        Categories = _repository.GetAllCategories();
        //check products remains
        var categoryCheckCookie = Request.Cookies["CategoryCheck"];
        
        CategoryCheck = string.IsNullOrEmpty(categoryCheckCookie)
            ? new List<string>()
            : JsonConvert.DeserializeObject<List<string>>(categoryCheckCookie);
        
        SelectedCategories = Categories.Where(c => CategoryCheck.Contains(c)).ToList();
        // shit shit shit shit happens
        SelectedCategories.Add("Fictitious category");
        
        PaginatedProducts = PaginatedList<Product>.Create(
            _repository.GetProductsByCategories(CategoryCheck).ToList(), pageIndex ?? 1, _pageSize);
        
        redirectUserString = "?login=" + Request.Query["login"] + "&isadmin=" + Request.Query["isadmin"];
    }
}