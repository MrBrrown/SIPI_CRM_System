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
        return _context.Products;
    }

    public List<string> GetAllCategories()
    {
        var categories = new List<string>();

        foreach (var product in _context.Products)
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
        var productToRemove = _context.Products.FirstOrDefault(x => x.Id.Equals(id));
        if (productToRemove != null)
            _context.Products.Remove(productToRemove);
        // ??????????????
        // delete product_dish
        // delete product
        // if exist one more we override id in product_dish
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

    // while think
    public void CheckRemains()
    {
        IEnumerable<Product> products = _context.Products.Where(x => x.Amount <= 0);
        _context.Products.RemoveRange(products);
        _context.SaveChanges();
    }

    public IEnumerable<Product> GetProductsByCategories(List<string> categoryCheck)
    {
        var products = new List<Product>();
        
        if (!categoryCheck.Any())
        {
            foreach (var product in _context.Products)
            {
                products.Add(product);
            }
        }
        else
        {
            foreach (var category in categoryCheck)
            {
                foreach (var product in _context.Products)
                {
                    if (product.Category.Equals(category))
                        products.Add(product);
                }
            }
        }

        return products;
    }
}