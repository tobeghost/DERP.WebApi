using System.Threading.Tasks;
using DERP.WebApi.Domain.Entities;

namespace DERP.Services.Abstract;

public interface ICustomerService
{
    Task<Customer> GetCustomerByUsername(string username);
    Task UpdateCustomerLastLoginDate(Customer customer);
    Task<string> ValidateCustomer(string username, string password);
    Task<string> RegisterCustomer(Customer customer);
    Task UpdateActive(string customerId, bool status);
    Task<Customer> GetCustomerById(string customerId);
}