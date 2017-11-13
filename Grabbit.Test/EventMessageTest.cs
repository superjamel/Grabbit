using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace Grabbit.Test
{
    [TestClass]
    public class ObjectExtensionTest
    {
        [TestMethod]
        public void ToByteArray_GivenEventMessageWith1Property_ShouldConvertToString()
        {
            var eventMessage = new EventMessage() { Body = "test"};

            var actual = eventMessage.ToByteArray();

            var expectedString = JsonConvert.SerializeObject(new EventMessage() {Body = "test"});
            var expectedByteArray = Encoding.UTF8.GetBytes(expectedString);

            Assert.AreEqual(expectedByteArray, actual);
        }
    }
}
