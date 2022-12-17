using System;
using System.Collections.Generic;
using System.Reflection;
using DERP.WebApi.Application.Abstract;
using DERP.WebApi.Application.Concrete;
using DERP.WebApi.Application.Dtos;

namespace DERP.WebApi.Application.Helpers;

public static class ErpClientHelper
{
    public static IErpClientHandler GetClient(string handlerClassAssembly, string handlerClassName)
    {
        if (string.IsNullOrEmpty(handlerClassAssembly))
        {
            handlerClassAssembly = Assembly.GetExecutingAssembly().FullName;
        }
        
        var instance = Activator.CreateInstance(handlerClassAssembly, handlerClassName).Unwrap();
        if (instance is BaseErpClient handler)
        {
            handler.Configuration = new ErpConfiguration()
            {
                
            };
        }
        
        return instance as IErpClientHandler;
    }
}