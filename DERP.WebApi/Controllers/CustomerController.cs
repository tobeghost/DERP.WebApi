using System.Threading.Tasks;
using DERP.WebApi.Application.Helpers;
using DERP.WebApi.Domain.Dtos.Customer;
using DERP.WebApi.Infrastructure.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DERP.WebApi.Controllers;

public class CustomerController : BaseController
{
    private readonly HttpRequestHelper _httpRequestHelper;

    public CustomerController(HttpRequestHelper httpRequestHelper)
    {
        _httpRequestHelper = httpRequestHelper;
    }

    [HttpPost("Create")]
    public async Task<IActionResult> CreateCustomer(CreateCustomerRequest createCustomerRequest)
    {
        var client = ErpClientHelper.GetClient("", "");
        if (client != null)
        {
            await client.CreateCustomer(createCustomerRequest);
        }
        
        return Ok();
    }

    [HttpGet("Detail")]
    public async Task<IActionResult> GetCustomer(string customerId)
    {
        return Ok();
    }

    [HttpDelete("Delete")]
    public async Task<IActionResult> DeleteCustomer(string customerId)
    {
        return Ok();
    }

    [HttpPost("Filter")]
    [ProducesResponseType(typeof(CustomerFilterResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> FilterCustomers(CustomerFilterRequest customerFilterRequest)
    {
        var username = _httpRequestHelper.GetUsername();

        return Ok();
    }
}