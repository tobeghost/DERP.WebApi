using DERP.WebApi.Application.Dtos;

namespace DERP.WebApi.Application.Concrete;

public abstract class BaseErpClient
{
    public virtual ErpConfiguration Configuration { get; set; }
}