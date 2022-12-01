using System;
using System.Threading.Tasks;
using DERP.Services.Abstract;
using DERP.WebApi.Domain.Entities;
using DERP.WebApi.Domain.Enums;
using DERP.WebApi.Infrastructure.Context;
using DERP.WebApi.Infrastructure.Settings;
using MongoDB.Driver;

namespace DERP.Services.Concrete;

public class CustomerService : ICustomerService
{
    private readonly DerpContext _derpContext;
    private readonly IEncryptionService _encryptionService;
    private readonly CustomerSettings _customerSettings;

    public CustomerService(DerpContext derpContext, IEncryptionService encryptionService,
        CustomerSettings customerSettings)
    {
        _derpContext = derpContext;
        _encryptionService = encryptionService;
        _customerSettings = customerSettings;
    }

    public virtual Task<Customer> GetCustomerByUsername(string username)
    {
        if (string.IsNullOrWhiteSpace(username))
            return Task.FromResult<Customer>(null);

        var filter = Builders<Customer>.Filter.Eq(x => x.Username, username);
        return _derpContext.Customer.Find(filter).FirstOrDefaultAsync();
    }
    
    public virtual async Task UpdateCustomerLastLoginDate(Customer customer)
    {
        if (customer == null)
            throw new ArgumentNullException("customer");

        var builder = Builders<Customer>.Filter;
        var filter = builder.Eq(x => x.Id, customer.Id);
        var update = Builders<Customer>.Update
            .Set(x => x.LastLoginDate, customer.LastLoginDate)
            .Set(x => x.FailedLoginAttempts, customer.FailedLoginAttempts)
            .Set(x => x.CannotLoginUntilDate, customer.CannotLoginUntilDate);

        await _derpContext.Customer.UpdateOneAsync(filter, update);
    }

    public virtual async Task<string> ValidateCustomer(string username, string password)
    {
        var customer = await GetCustomerByUsername(username);

        if (customer == null)
            return "CustomerNotExist";
        
        if (customer.Deleted)
            return "Deleted";
        
        if (!customer.Active)
            return "NotActive";

        if (customer.CannotLoginUntilDate.HasValue && customer.CannotLoginUntilDate.Value > DateTime.Now)
            return "LockedOut";

        var pwd = customer.PasswordFormat switch
        {
            PasswordFormat.Encrypted => _encryptionService.EncryptText(password),
            PasswordFormat.Hashed => _encryptionService.CreatePasswordHash(password, customer.PasswordSalt, _customerSettings.HashedPasswordFormat),
            _ => password
        };

        var isValid = pwd == customer.Password;
        if (!isValid)
        {
            customer.FailedLoginAttempts++;
            
            if (_customerSettings.FailedPasswordAllowedAttempts > 0 && customer.FailedLoginAttempts >= _customerSettings.FailedPasswordAllowedAttempts)
            {
                //lock out
                customer.CannotLoginUntilDate = DateTime.Now.AddMinutes(_customerSettings.FailedPasswordLockoutMinutes);
                
                //reset the counter
                customer.FailedLoginAttempts = 0;
            }

            await UpdateCustomerLastLoginDate(customer);
            return "WrongPassword";
        }

        //save last login date
        customer.FailedLoginAttempts = 0;
        customer.CannotLoginUntilDate = null;
        customer.LastLoginDate = DateTime.Now;
        await UpdateCustomerLastLoginDate(customer);
        return "Successful";
    }
}