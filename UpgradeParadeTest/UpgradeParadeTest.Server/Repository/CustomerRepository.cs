using Microsoft.EntityFrameworkCore;
using UpgradeParadeTest.Server.Data;
using UpgradeParadeTest.Server.Interfaces;
using UpgradeParadeTest.Server.Models;
using static System.Net.Mime.MediaTypeNames;

namespace UpgradeParadeTest.Server.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly DatabaseContext _dbContext;

        public CustomerRepository(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
        {
            var customers = await _dbContext.Customers.ToListAsync();
            return customers;
        }
   
        public async Task<Customer> GetCustomerById(Guid id)
        {
            var customer = await _dbContext.Customers.FirstOrDefaultAsync(i => i.Id == id);
            return customer;
        }

        public IEnumerable<Customer> GetAllCustomers()
        {
            var customers = _dbContext.Customers.ToList();
            return customers;
        }
        public async Task AddCustomerAsync(Customer customer)
        {
            _dbContext.Customers.Add(customer);
            await _dbContext.SaveChangesAsync();
        }

    }
}
