using Microsoft.EntityFrameworkCore;
using SIPI_CRM_System.Models;

namespace SIPI_CRM_System.Services.ProductRep;

public class DbProductRepository : IProductRepository
{
    private readonly CRMdbContext _context;

    public DbProductRepository(CRMdbContext context)
    {
        _context = context;
    }

    public IEnumerable<Product> GetProducts()
    {
        List<Product> products = new List<Product>();
        foreach (var product in _context.Products)
        {
            products.Add(product);
        }

        return products;
    }

    public List<string> GetAllCategories()
    {
        List<string> categories = new List<string>();

        foreach (var product in _context.Products)
        {
            if (!categories.Any(x => x.Equals(product.Category)))
            {
                categories.Add(product.Category);
            }
        }

        return categories;
    }

    public Product GetProductById(int id)
    {
        var product = _context.Products.FirstOrDefault(x => x.Id.Equals(id));
        return product;
    }

    public int GetLastProductId()
    {
        return _context.Products.OrderBy(x => x.Id).Last().Id;
    }

    public void AddProduct(Product product)
    {
        if (!_context.Products.Any(x => x.Name.Equals(product.Name)))
            _context.Products.Add(product);
        
        _context.SaveChanges();
    }

    public void RemoveProductById(int id)
    {
        var productToRemove = _context.Products.FirstOrDefault(x => x.Id.Equals(id));
        if (productToRemove != null)
            _context.Products.Remove(productToRemove);
        
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
        List<Product> products = new List<Product>();
        
        if (categoryCheck.Count() == 0)
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