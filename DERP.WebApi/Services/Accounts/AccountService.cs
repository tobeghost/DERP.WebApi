using DERP.WebApi.Domain.Entities;
using DERP.WebApi.Infrastructure.Context;
using MongoDB.Driver;

namespace DERP.Services.Accounts;

public class AccountService : IAccountService
{
    private readonly DerpContext _derpContext;

    public AccountService(DerpContext derpContext)
    {
        _derpContext = derpContext;
    }

    public void Create()
    {
        var dto = new Account
        {
            Name = "dsadas"
        };

        _derpContext.Account.InsertOne(dto);
    }
}