using System.Collections.Generic;
using System.Threading.Tasks;
using DERP.WebApi.Application.Abstract;
using DERP.WebApi.Application.Dtos.Zirve;
using DERP.WebApi.Domain.Dtos.Customer;
using DERP.WebApi.Infrastructure.Context;

namespace DERP.WebApi.Application.Concrete;

public class ZirveClient : BaseErpClient, IErpClientHandler
{
    /* Zirve Db Tables
     * 2022T.dbo.CARIGEN -> Cari listesinin bulunduğu tablo
     * 2022T.dbo.CBKISLEM -> Cari kaydına ait işlem listesinin olduğu tablo
     * 2022T.dbo.ISLEMKOD -> Cari işlem listesindeki ISLEMKODU alanına ait verilerin olduğu tablo
     */
    
    public async Task CreateCustomer(CreateCustomerRequest createCustomerRequest)
    {
        await Task.Delay(1000);
    }

    public async Task<CustomerFilterResponse> FilterCustomers(CustomerFilterRequest customerFilterRequest)
    {
        using var ctx = new DapperContext(base.Configuration);
        
        var accounts = await ctx.Query<CARIGEN>("SELECT * FROM dbo.LISTECARILER()");

        var response = new CustomerFilterResponse();
        
        var result = new List<CustomerDto>();

        foreach (var account in accounts)
        {
            var row = new CustomerDto();
            row.Code = account.CRK;
            row.Name = account.STA;
            row.Address = account.ADRES1;
            row.Phone = account.TELEFON;
            
            result.Add(row);
        }

        response.Items = result;

        return response;
    }
}