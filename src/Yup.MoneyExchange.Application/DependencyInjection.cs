using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Yup.MoneyExchange.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddAplicacion(this IServiceCollection services, IConfiguration configuration)
    {
        //MetiatR DependencyInjection
        services.AddMediatR(Assembly.GetExecutingAssembly());


        return services;
    }
}
