using UpgradeParadeTest.Server.Models;

namespace UpgradeParadeTest.Server.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<Product> GetProductById(Guid id);
        Task<Product> GetProductByName(string productName);
    }
}
