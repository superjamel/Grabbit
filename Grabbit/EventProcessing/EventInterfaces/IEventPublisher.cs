using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Grabbit.EventProcessing.EventInterfaces
{
    public interface IEventPublisher
    {
        Task PublishEventAsync(EventMessage message, string routingKey);
        
    }
}
