using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Grabbit.Attributes;

namespace Grabbit
{
    public class EventAttributeBinder
    {
        public void BindAllMethods()
        {
            var types = GetAllMethodsWithEventAttribute();
        }

        public IEnumerable<MethodInfo> GetAllMethodsWithEventAttribute()
        {
            return Assembly.GetCallingAssembly()
                .GetTypes()
                .SelectMany(GetMethods)
                .Where(HasEventAttribute);
        }

        private static MethodInfo[] GetMethods(Type type)
        {
            return type.GetMethods();
        }

        private static bool HasEventAttribute(MethodInfo methodInfo)
        {
            return methodInfo.GetCustomAttributes<EventAttribute>().Any();
        }
    }
}
