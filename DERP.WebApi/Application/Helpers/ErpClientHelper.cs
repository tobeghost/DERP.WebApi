using System;
using System.Collections.Generic;
using System.Reflection;
using DERP.WebApi.Application.Abstract;
using DERP.WebApi.Application.Concrete;
using DERP.WebApi.Domain.Entities;

namespace DERP.WebApi.Application.Helpers;

public static class ErpClientHelper
{
    public static IErpClientHandler GetClient(this Customer customer)
    {
        if (customer.ErpConfiguration == null)
            throw new ArgumentNullException("Not found erp configuration for customer");

        var handlerClassAssembly = customer.ErpConfiguration.HandlerClassAssembly;
        var handlerClassName = customer.ErpConfiguration.HandlerClassName;
        
        if (string.IsNullOrEmpty(handlerClassAssembly))
        {
            handlerClassAssembly = Assembly.GetExecutingAssembly().FullName;
        }
        
        var instance = Activator.CreateInstance(handlerClassAssembly, handlerClassName).Unwrap();
        if (instance is BaseErpClient handler)
        {
            handler.Configuration = customer.ErpConfiguration;
        }
        
        return instance as IErpClientHandler;
    }
}