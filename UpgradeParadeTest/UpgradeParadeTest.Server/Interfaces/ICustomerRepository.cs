using System.Threading.Tasks;
using UpgradeParadeTest.Server.Models;

namespace UpgradeParadeTest.Server.Interfaces
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<Customer>> GetAllCustomersAsync();
        Task<Customer> GetCustomerById(Guid id);
        IEnumerable<Customer> GetAllCustomers();
        Task AddCustomerAsync(Customer customer);
    }
}
