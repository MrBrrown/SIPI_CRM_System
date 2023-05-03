using SIPI_CRM_System.Models;

namespace SIPI_CRM_System.Services.ProductRep;

public interface IProductRepository
{
    IEnumerable<Product> GetProducts();
    List<string> GetAllCategories();
    Product GetProductById(int id);
    int GetLastProductId();
    void AddProduct(Product product);
    void RemoveProductById(int id);
    void Update(Product product);
    IEnumerable<Product> SetCheck(List<string> categoryCheck);
}