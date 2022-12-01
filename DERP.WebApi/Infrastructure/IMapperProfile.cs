namespace DERP.WebApi.Infrastructure;

public interface IMapperProfile
{
    /// <summary>
    /// Gets order of this configuration implementation
    /// </summary>
    int Order { get; }
}