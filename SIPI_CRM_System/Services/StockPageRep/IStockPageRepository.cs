using SIPI_CRM_System.Models;
using SIPI_CRM_System.Pagination;

namespace SIPI_CRM_System.Services.StockPageRep;

public interface IStockPageRepository
{
    IEnumerable<Product> GetProducts();

    List<string> GetAllCategories();

    Product? GetProductById(int id);

    void AddProduct(Product product);

    void RemoveProductById(int id);

    void Update(Product product);

    Task UpdateProductFitAsync(Product product);

    IEnumerable<Product> GetProductsByCategories(List<string> categoryCheck);
}