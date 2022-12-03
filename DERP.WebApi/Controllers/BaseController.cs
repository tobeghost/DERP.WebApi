using System.Collections.Generic;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace DERP.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public abstract class BaseController : ControllerBase
{
    public IEnumerable<T> FindServices<T>()
    {
        return HttpContext.RequestServices.GetServices<T>();
    }

    public T FindService<T>()
    {
        return HttpContext.RequestServices.GetService<T>();
    }

    protected virtual IActionResult InvokeHttp404()
    {
        Response.StatusCode = 404;
        return new EmptyResult();
    }

    public override ForbidResult Forbid()
    {
        return new ForbidResult(JwtBearerDefaults.AuthenticationScheme);
    }
}