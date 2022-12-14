using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using DERP.WebApi.Domain.Dtos.Customer;

namespace DERP.WebApi.Application.Abstract;

public interface IErpClientHandler
{
    Task CreateCustomer(CreateCustomerRequest createCustomerRequest);
    Task<CustomerFilterResponse> FilterCustomers(CustomerFilterRequest customerFilterRequest);
}