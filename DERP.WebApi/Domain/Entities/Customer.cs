using System;
using DERP.WebApi.Domain.Common;
using DERP.WebApi.Domain.Enums;

namespace DERP.WebApi.Domain.Entities;

public class Customer : MongoBaseEntity
{
    /// <summary>
    /// Gets or sets the username
    /// </summary>
    public string Username { get; set; }

    /// <summary>
    /// Gets or sets the password
    /// </summary>
    public string Password { get; set; }

    /// <summary>
    /// Gets or sets the password format
    /// </summary>
    public PasswordFormat PasswordFormat { get; set; }

    /// <summary>
    /// Gets or sets the password salt
    /// </summary>
    public string PasswordSalt { get; set; }

    /// <summary>
    /// Gets or sets the admin comment
    /// </summary>
    public string AdminComment { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the customer is active
    /// </summary>
    public bool Active { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the customer has been deleted
    /// </summary>
    public bool Deleted { get; set; }

    /// <summary>
    /// Gets or sets a value indicating number of failed login attempts (wrong password)
    /// </summary>
    public int FailedLoginAttempts { get; set; }

    /// <summary>
    /// Gets or sets the date and time until which a customer cannot login (locked out)
    /// </summary>
    public DateTime? CannotLoginUntilDate { get; set; }

    /// <summary>
    /// Gets or sets the customer system name
    /// </summary>
    public string SystemName { get; set; }

    /// <summary>
    /// Gets or sets the last IP address
    /// </summary>
    public string LastIpAddress { get; set; }

    /// <summary>
    /// Gets or sets the date and time of last login
    /// </summary>
    public DateTime? LastLoginDate { get; set; }

    /// <summary>
    /// Gets or sets the date and time of last activity
    /// </summary>
    public DateTime LastActivityDate { get; set; }

    /// <summary>
    /// Gets or sets the date and time of last purchase
    /// </summary>
    public DateTime? LastPurchaseDate { get; set; }

    /// <summary>
    /// Last date to change password
    /// </summary>
    public DateTime? PasswordChangeDate { get; set; }
    
    /// <summary>
    /// Last erp configuration for clients
    /// </summary>
    public ErpConfiguration ErpConfiguration { get; set; }
}