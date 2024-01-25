using UpgradeParadeTest.Server.Models;

namespace UpgradeParadeTest.Server.Interfaces
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> GetAllOrdersAsync();
        Task<Order> GetOrderById(Guid id);
        Task AddOrderAsync(Order order);
        IEnumerable<Order> GetAllOrders();
    }
}
