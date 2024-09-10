using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market.EventBus;
public interface IEventSender
{
    Task<SendResult> SendAsync<TMessage>(TMessage message) where TMessage : BaseIntegrationEvent;
}
