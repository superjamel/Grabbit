using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Grabbit.EventProcessing.EventInterfaces
{
    public interface IEventPublisher
    {
        Task PublishEventAsync(object eventBody, string routingKey);
        
    }
}
