using Microsoft.EntityFrameworkCore;
using SIPI_CRM_System.Models;
using SIPI_CRM_System.Pagination;

namespace SIPI_CRM_System.Services.StockPage;

public class DataBaseStockPageRepository : IStockPageRepository 
{
    private readonly CRMdbContext _context;

    public DataBaseStockPageRepository(CRMdbContext context)
    {
        _context = context;
    }

    public IEnumerable<Product> GetProducts()
    {
        return _context.Products.Where(x => x.Amount > 0).ToList();
    }

    public List<string> GetAllCategories()
    {
        var categories = new List<string>();

        foreach (var product in _context.Products.Where(x => x.Amount > 0).ToList())
        {
            if (!categories.Any(x => x.Equals(product.Category)))
            {
                categories.Add(product.Category);
            }
        }

        return categories;
    }

    public Product? GetProductById(int id)
    {
        var product = _context.Products.FirstOrDefault(x => x.Id.Equals(id));
        return product;
    }

    public void AddProduct(Product product)
    {
        _context.Products.Add(product);
        
        _context.SaveChanges();
    }

    public void RemoveProductById(int id)
    {
        /*var productToRemove = _context.Products.FirstOrDefault(x => x.Id.Equals(id));
        if (productToRemove != null)
            _context.Products.Remove(productToRemove);*/
        
        _context.Products.FirstOrDefault(x => x.Id.Equals(id)).Amount = 0;
        
        _context.SaveChanges();
    }

    public void Update(Product product)
    {
        _context.Products
            .Where(x => x.Id == product.Id)
            .ForEachAsync(x => {
                x.Name = product.Name;
                x.Amount = product.Amount;
                x.Category = product.Category; });
        
        _context.SaveChanges();
    }

    public IEnumerable<Product> GetProductsByCategories(List<string> categoryCheck)
    {
        var products = new List<Product>();
        
        if (!categoryCheck.Any())
        {
            foreach (var product in _context.Products.Where(x => x.Amount > 0).ToList())
            {
                products.Add(product);
            }
        }
        else
        {
            foreach (var category in categoryCheck)
            {
                foreach (var product in _context.Products.Where(x => x.Amount > 0).ToList())
                {
                    if (product.Category.Equals(category))
                        products.Add(product);
                }
            }
        }

        return products;
    }
    
    public async Task UpdateProductFitAsync(Product product)
    {
        _context.Products.FirstOrDefault(x => x.Id == product.Id).IsFit = false;

        await _context.SaveChangesAsync();
    }
}