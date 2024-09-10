using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Market.EventBus.RabbitMq;

public class RabbitMqEventSender : IEventSender
{
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly ILogger<RabbitMqEventSender> _logger;

    public RabbitMqEventSender(IPublishEndpoint publishEndpoint, ILogger<RabbitMqEventSender> logger)
    {
        _publishEndpoint = publishEndpoint;
        _logger = logger;
    }

    public Task<SendResult> SendAsync<TMessage>(TMessage message) where TMessage : BaseIntegrationEvent
    {
        try
        {
            _publishEndpoint.Publish(message);
            return Task.FromResult(SendResult.Acknowledged);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error sending message. Id: {message.Id}. CorrelationId: {message.CorrelationId}");
            return Task.FromResult(SendResult.NoneRecoverableFailure);
        }
    }
}
