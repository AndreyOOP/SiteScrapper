using Microsoft.VisualStudio.TestTools.UnitTesting;
using SiteParsingHelper.Event;
using SiteParsingHelperTests.EventPrototype;
using System;

namespace SiteParsingHelperTests
{
    [TestClass]
    public class WebParserPrototypeTests
    {
        [TestMethod]
        public void SampleOfParserSetupAndExecution()
        {
            var webParser = new WebParser<ParsingResult>();

            webParser.RegisterUnit<ModelInA, ModelInB>(new UnitA(webParser));
            webParser.RegisterUnit<ModelInB, ModelInC>(new UnitB(webParser));

            webParser.ExecuteUnit<ModelInA, ModelInB>(new ModelInA());

            Assert.AreEqual("a", webParser.Result.A);
            Assert.AreEqual("b", webParser.Result.B);
            Assert.AreEqual("UnitA > UnitB", webParser.ExecutionPath.ToString());
        }

        [TestMethod]
        public void RegisterUnit_RegisterSameUnitTwice_ArgumentException()
        {
            var eventBus = new WebParser<ParsingResult>();

            var exception = Assert.ThrowsException<ArgumentException>(() =>
            {
                eventBus.RegisterUnit<ModelInA, ModelInB>(new UnitA(eventBus));
                eventBus.RegisterUnit<ModelInA, ModelInB>(new UnitA(eventBus));
            });
            StringAssert.EndsWith(exception.Message, "already set");
        }

        [TestMethod]
        public void ExecuteUnit_ExecuteNotRegisteredUnit_ArgumentExceptionInExceptionProperty()
        {
            var eventBus = new WebParser<ParsingResult>();
            eventBus.RegisterUnit<ModelInA, ModelInB>(new UnitA(eventBus));

            eventBus.ExecuteUnit<ModelInB, ModelInC>(new ModelInB());

            Assert.AreEqual(typeof(ArgumentException), eventBus.Exception.GetType());
            StringAssert.EndsWith(eventBus.Exception.Message, "is not registered");
        }

        // ToDo: add cycle tests
    }
}
