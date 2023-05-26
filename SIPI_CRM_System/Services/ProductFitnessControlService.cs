using SIPI_CRM_System.Services.StockPage;
using SIPI_CRM_System.Services.StockPageRep;

namespace SIPI_CRM_System.Services;

public class ProductFitnessControlService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    
    public ProductFitnessControlService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var stockPageRepository = scope.ServiceProvider.GetRequiredService<IStockPageRepository>();
                await ControlFitness(stockPageRepository);
                
                await Task.Delay(TimeSpan.FromSeconds(30), stoppingToken);
            }
        }
    }
    
    private async Task ControlFitness(IStockPageRepository stockPageRepository)
    {
        try
        {
            var products = stockPageRepository.GetProducts();

            foreach (var product in products)
            {
                var expirationDate = product.DeliveryDateTime.AddHours(product.LifeTime);
                var remainingTime = expirationDate - DateTime.Now;

                if (remainingTime <= TimeSpan.Zero)
                {
                    await stockPageRepository.UpdateProductFitAsync(product);
                }
            }
        }
        catch (Exception e)
        {
            // ignored
        }
    }
}