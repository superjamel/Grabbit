using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Grabbit.Extension
{
    public static class ObjectExtension
    {
        /// <summary>
        /// Converts the given object to a byte.
        /// </summary>
        /// <returns></returns>
        public static byte[] ConvertObjectToByte(this object objectToConvert)
        {
            string jsonValue = JsonConvert.SerializeObject(objectToConvert);
            byte[] byteValue = Encoding.UTF8.GetBytes(jsonValue);
            return byteValue;
        }
    }
}
