using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Grabbit.Test
{
    [TestClass]
    public class EventAttributeBinderTest
    {
        [TestMethod]
        public void GetAllMethodsWithAttribute_GivenTestController_ShouldReturnTestControllerMethod()
        {
            var sut = new EventAttributeBinder();

            var actualMethods = sut.GetAllMethodsWithEventAttribute();

            var methodNames = actualMethods.Select(mi => mi.Name).ToList();
            CollectionAssert.Contains(methodNames, "HandleEvent");
        }
    }
}
