using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Grabbit.EventProcessing.EventInterfaces
{
    public interface IEventBus
    {
        void PublishEventAsync(EventMessage message);

        void ConsumeEvent(string topic, string routing, Action<EventMessage> callback);

    }
}
