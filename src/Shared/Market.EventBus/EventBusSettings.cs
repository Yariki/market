using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market.EventBus;
public record EventBusSettings
{
    public string? HostAddress { get; init; }
    public string? Username { get; init; }
    public string? Password { get; init; }
    public string? QueueName { get; init; }
}
