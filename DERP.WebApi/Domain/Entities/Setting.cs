using DERP.WebApi.Domain.Common;

namespace DERP.WebApi.Domain.Entities;

public class Setting : MongoBaseEntity
{
    public Setting(string name, string value)
    {
        this.Name = name;
        this.Value = value;
    }

    /// <summary>
    /// Gets or sets the name
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the value
    /// </summary>
    public string Value { get; set; }

    public override string ToString()
    {
        return Name;
    }
}