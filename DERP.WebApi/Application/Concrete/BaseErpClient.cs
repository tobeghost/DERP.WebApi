using DERP.WebApi.Domain.Entities;

namespace DERP.WebApi.Application.Concrete;

public abstract class BaseErpClient
{
    public virtual ErpConfiguration Configuration { get; set; }
}