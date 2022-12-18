using System.Threading.Tasks;
using DERP.WebApi.Application.Abstract;
using DERP.WebApi.Domain.Dtos.Customer;
using DERP.WebApi.Infrastructure.Context;

namespace DERP.WebApi.Application.Concrete;

public class ZirveClient : BaseErpClient, IErpClientHandler
{
    public async Task CreateCustomer(CreateCustomerRequest createCustomerRequest)
    {
        using var ctx = new DapperContext(base.Configuration);
        
        var accounts = await ctx.Query("SELECT * FROM TAccount");
        
        await Task.Delay(1000);
    }
}