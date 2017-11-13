using System;
using System.Collections.Generic;
using System.Text;
using Grabbit.Attributes;

namespace Grabbit.Test.FakeTestClasses
{
    internal class TestController
    {
        /// <summary>
        /// Should be called when the event routing key receives an event
        /// </summary>
        /// <param name="message"></param>
        [Event("*.log.*")]
        public void HandleEvent(WaitHandleEventMessage message)
        {
            message.WaitHandle.Set();
        }

        public void OtherMethod(WaitHandleEventMessage message)
        {
            // Should not be called
        }
    }
}
