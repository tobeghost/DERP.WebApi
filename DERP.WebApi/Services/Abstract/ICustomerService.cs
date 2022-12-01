using System.Threading.Tasks;
using DERP.WebApi.Domain.Entities;

namespace DERP.Services.Abstract;

public interface ICustomerService
{
    Task<Customer> GetCustomerByUsername(string username);
    Task UpdateCustomerLastLoginDate(Customer customer);
    Task<string> ValidateCustomer(string username, string password);
}