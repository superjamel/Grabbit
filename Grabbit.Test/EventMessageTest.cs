using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace Grabbit.Test
{
    [TestClass]
    public class EventMessageTest
    {
        [TestMethod]
        public void ToByteArray_GivenEventMessageWith1Property_ShouldConvertToString()
        {
            var eventMessage = new EventMessage() { Body = "test"};

            var actual = eventMessage.BodyToByteArray();
            

            CollectionAssert.AreEqual(Encoding.UTF8.GetBytes("test"), actual);
        }
    }
}
