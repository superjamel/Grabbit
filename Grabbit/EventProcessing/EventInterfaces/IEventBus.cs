using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Grabbit.EventProcessing.EventInterfaces
{
    public interface IEventBus
    {
        void BasicPublish(EventMessage message);

        void BasicConsume(string topic, string routing, Action<EventMessage> callback);

    }
}
