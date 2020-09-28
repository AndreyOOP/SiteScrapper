using Microsoft.VisualStudio.TestTools.UnitTesting;
using ParserCore.Attributes;
using ParserCore.Extensions;
using System;
using System.Linq;
using System.Reflection;

namespace UnitTests
{
    [TestClass]
    public class GetCustomAttributeDataTests
    {
        class CustomAttributeAAttribute : Attribute { }
        class CustomAttributeBAttribute : Attribute { }
        class UsageOfAttributes
        {
            [CustomAttributeA]
            [CustomAttributeB]
            public int A { get; set; }

            public int B { get; set; }
            
            [CustomAttributeA]
            private int C { get; set; }
        }

        [TestMethod]
        public void GetCustomAttributeData_AttributesOfAProperty_GetCustomAttributes()
        {
            var properties = typeof(UsageOfAttributes).GetProperties();
            var propertyA = properties.First(p => p.Name == "A");

            var propAattrA = propertyA.GetCustomAttributeData<CustomAttributeAAttribute>();
            var propAattrB = propertyA.GetCustomAttributeData<CustomAttributeBAttribute>();

            Assert.AreEqual(typeof(CustomAttributeAAttribute), propAattrA.AttributeType);
            Assert.AreEqual(typeof(CustomAttributeBAttribute), propAattrB.AttributeType);
        }

        [TestMethod]
        public void GetCustomAttributeData_NoAttribute_Null()
        {
            var propertyB = typeof(UsageOfAttributes).GetProperties().First(p => p.Name == "B");

            var result = propertyB.GetCustomAttributeData<CustomAttributeAAttribute>();

            Assert.IsNull(result);
        }

        [TestMethod]
        public void GetCustomAttributeData_PrivateProperty_GetAttribute()
        {
            // note flags combination to get private property
            var propertyC = typeof(UsageOfAttributes).GetProperties(BindingFlags.NonPublic | BindingFlags.Instance).First(p => p.Name == "C");

            var propCattrA = propertyC.GetCustomAttributeData<CustomAttributeAAttribute>();

            Assert.AreEqual(typeof(CustomAttributeAAttribute), propCattrA.AttributeType);
        }
    }

    [TestClass]
    public class GetConstructorArgumentTests 
    {
        class TestAttribute : Attribute
        {
            public TestAttribute() { }
            public TestAttribute(int a) { }
            public TestAttribute(int a, int b, string c) { }
        }

        class Test
        {
            [Test]
            public int PropA { get; set; }

            [Test(1)]
            public int PropB { get; set; }

            [Test(1, 2, "z")]
            public int PropC { get; set; }
        }

        [TestMethod]
        public void GetCustomAttributeData_NoConstructor_Null()
        {
            var attributePropA = typeof(Test).GetProperties().First(p => p.Name == "PropA");
            var attributeData = attributePropA.GetCustomAttributeData<TestAttribute>();

            Assert.ThrowsException<ArgumentOutOfRangeException>(() => attributeData.GetConstructorArgument<int>());
        }

        [TestMethod]
        public void GetCustomAttributeData_ConstructorWithSingleIntArgument_ExpectedArgumentValue()
        {
            var attributePropB = typeof(Test).GetProperties().First(p => p.Name == "PropB");
            var attributeData = attributePropB.GetCustomAttributeData<TestAttribute>();

            var constructorArgument = attributeData.GetConstructorArgument<int>();

            Assert.AreEqual(1, constructorArgument);
        }

        [TestMethod]
        public void GetCustomAttributeData_ConstructorWithMultipleArgument_ExpectedArgumentValue()
        {
            var attributePropC = typeof(Test).GetProperties().First(p => p.Name == "PropC");
            var attributeData = attributePropC.GetCustomAttributeData<TestAttribute>();

            Assert.AreEqual(1, attributeData.GetConstructorArgument<int>());
            Assert.AreEqual(2, attributeData.GetConstructorArgument<int>(1));
            Assert.AreEqual("z", attributeData.GetConstructorArgument<string>(2));
        }

        [TestMethod]
        public void GetConstructorArgument_IncorrectTypeOnPosition_InvalidCastException()
        {
            var attributePropC = typeof(Test).GetProperties().First(p => p.Name == "PropC");
            var attributeData = attributePropC.GetCustomAttributeData<TestAttribute>();

            Assert.ThrowsException<InvalidCastException>(() => attributeData.GetConstructorArgument<string>(0));
        }
    }

    [TestClass]
    public class AttributeExtensionTests
    {
        class TestXPathAttribute
        {
            [XPath("//div[@class='top_text']")]
            public int PropA { get; set; }
        }

        [TestMethod]
        public void GetConstructorArgument_XPathAttribute_GetXPathAttributeValue()
        {
            var testType = new TestXPathAttribute();
            var propInfo = testType.GetType().GetProperties().First();
            var attributeData = propInfo.GetCustomAttributeData<XPathAttribute>();

            var constructorArgumentValue = attributeData.GetConstructorArgument<string>();

            Assert.AreEqual("//div[@class='top_text']", constructorArgumentValue);
        }
    }
}
