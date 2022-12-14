using System.Threading.Tasks;
using DERP.Services.Abstract;
using DERP.WebApi.Application.Helpers;
using DERP.WebApi.Domain.Dtos.Customer;
using DERP.WebApi.Infrastructure.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DERP.WebApi.Controllers;

public class CustomerController : BaseController
{
    private readonly HttpRequestHelper _httpRequestHelper;
    private readonly ICustomerService _customerService;

    public CustomerController(HttpRequestHelper httpRequestHelper, ICustomerService customerService)
    {
        _httpRequestHelper = httpRequestHelper;
        _customerService = customerService;
    }

    [HttpGet("Detail")]
    public async Task<IActionResult> GetCustomer(string customerId)
    {
        return Ok();
    }

    [HttpPost("Filter")]
    [ProducesResponseType(typeof(CustomerFilterResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> FilterCustomers(CustomerFilterRequest customerFilterRequest)
    {
        var username = _httpRequestHelper.GetUsername();

        var customer = await _customerService.GetCustomerByUsername(username);

        var client = customer.GetClient();
        if (client != null)
        {
            var data = await client.FilterCustomers(customerFilterRequest);
            return Ok(data);
        }

        return Ok();
    }
}