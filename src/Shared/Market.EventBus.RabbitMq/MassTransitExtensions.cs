using System.Reflection;
using MassTransit;
using MassTransit.SagaStateMachine;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Market.EventBus.RabbitMq;

public static class MassTransitExtensions
{
    public static IServiceCollection AddServiceBus(this IServiceCollection services, IConfiguration configuration, bool addConsumers = false)
    {
        services.AddMassTransit(x =>
        {
            if (addConsumers)
            {
                var entryAssembly = Assembly.GetEntryAssembly();
                x.AddConsumers(entryAssembly);
            }

            x.SetKebabCaseEndpointNameFormatter();
            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(configuration["EventBusSettings:HostAddress"], "/");
                cfg.ConfigureEndpoints(context);
            });
        });
        return services;
    }

    public static IServiceCollection AddEventBusSender(this IServiceCollection services)
    {
        services.AddScoped<IEventSender, RabbitMqEventSender>();
        return services;
    }
}
