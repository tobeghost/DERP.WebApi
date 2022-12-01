using System.Collections.Generic;

namespace DERP.WebApi.Infrastructure.Settings;

public class SecuritySettings : ISettings
{

    /// <summary>
    /// Gets or sets an encryption key
    /// </summary>
    public string EncryptionKey { get; set; }

    /// <summary>
    /// Gets or sets a list of admin area allowed IP addresses
    /// </summary>
    public List<string> AdminAreaAllowedIpAddresses { get; set; }
}