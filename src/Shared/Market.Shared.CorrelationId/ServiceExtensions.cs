using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Market.Shared.CorrelationId;
public static class ServiceExtensions
{

    public static IServiceCollection AddCorrelationIdServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<ICorrelationIdService, CorrelationIdService>();

        return serviceCollection;
    }
}
