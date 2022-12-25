using System.Collections.Generic;
using System.Threading.Tasks;
using DERP.WebApi.Application.Abstract;
using DERP.WebApi.Domain.Dtos.Customer;

namespace DERP.WebApi.Application.Concrete;

public class LogoClient : BaseErpClient, IErpClientHandler
{
    public async Task CreateCustomer(CreateCustomerRequest createCustomerRequest)
    {
        throw new System.NotImplementedException();
    }
    
    public async Task<CustomerFilterResponse> FilterCustomers(CustomerFilterRequest customerFilterRequest)
    {
        throw new System.NotImplementedException();
    }
}