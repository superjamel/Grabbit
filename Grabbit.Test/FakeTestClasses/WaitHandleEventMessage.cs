using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Grabbit.Test.FakeTestClasses
{
    internal class WaitHandleEventMessage : EventMessage
    {
        public ManualResetEvent WaitHandle { get; set; } = new ManualResetEvent(false);
    }
}
