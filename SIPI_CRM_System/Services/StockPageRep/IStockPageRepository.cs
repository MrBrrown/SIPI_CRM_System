using SIPI_CRM_System.Models;

namespace SIPI_CRM_System.Services.StockPage;

public interface IStockPageRepository
{
    IEnumerable<Product> GetProducts();

    List<string> GetAllCategories();

    Product GetProductById(int id);

    void AddProduct(Product product);

    void RemoveProductById(int id);

    void Update(Product product);

    IEnumerable<Product> GetProductsByCategories(List<string> categoryCheck);
}