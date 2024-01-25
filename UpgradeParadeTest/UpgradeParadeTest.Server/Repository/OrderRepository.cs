using Microsoft.EntityFrameworkCore;
using UpgradeParadeTest.Server.Data;
using UpgradeParadeTest.Server.Interfaces;
using UpgradeParadeTest.Server.Models;

namespace UpgradeParadeTest.Server.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly DatabaseContext _dbContext;

        public OrderRepository(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddOrderAsync(Order order)
        {
            _dbContext.Orders.Add(order);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            var orders = await _dbContext.Orders.ToListAsync();
            return orders;
        }

        public IEnumerable<Order> GetAllOrders()
        {
            var orders = _dbContext.Orders.ToList();
            return orders;
        }

        public async Task<Order> GetOrderById(Guid id)
        {
            var order = await _dbContext.Orders.FirstOrDefaultAsync(i => i.Id == id);
            return order;
        }

    }
}
