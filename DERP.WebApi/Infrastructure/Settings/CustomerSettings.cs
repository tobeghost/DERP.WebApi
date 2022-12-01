using DERP.WebApi.Domain.Enums;

namespace DERP.WebApi.Infrastructure.Settings;

public class CustomerSettings : ISettings
{
    /// <summary>
    /// Default password format for customers
    /// </summary>
    public PasswordFormat DefaultPasswordFormat { get; set; }

    /// <summary>
    /// Gets or sets a customer password format (SHA1, MD5) when passwords are hashed
    /// </summary>
    public string HashedPasswordFormat { get; set; }

    /// <summary>
    /// Gets or sets a minimum password length
    /// </summary>
    public int PasswordMinLength { get; set; }

    /// <summary>
    /// Gets or sets a number of days for password recovery link. Set to 0 if it doesn't expire.
    /// </summary>
    public int PasswordRecoveryLinkDaysValid { get; set; }

    /// <summary>
    /// User registration type
    /// </summary>
    public UserRegistrationType UserRegistrationType { get; set; }

    /// <summary>
    /// Gets or sets maximum login failures to lockout account. Set 0 to disable this feature
    /// </summary>
    public int FailedPasswordAllowedAttempts { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public int FailedPasswordLockoutMinutes { get; set; }

    /// <summary>
    /// Gets or sets a number of days for password expiration
    /// </summary>
    public int PasswordLifetime { get; set; }
}