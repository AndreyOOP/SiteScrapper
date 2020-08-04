using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ParserCore;

namespace ParserCoreTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var dict = new Dictionary<IInOutKey, object>
            {
                [new InOutKey<A, B>()] = new WorkerAB(),
                [new InOutKey<B, C>()] = new WorkerBC()
            };
            var container = new WorkersContainer(dict);

            var lst = container.Last;

            //var parser = new ConcreeteParser(container);
            var parser = new Parser<A, C>(container);

            var res = parser.Parse(new A());
        }
    }

    public class ConcreeteParser : Parser<A, Result>
    {
        public ConcreeteParser(IWorkersContainer workersContainer) : base(workersContainer)
        {
        }

        protected override Result PrepareResult()
        {
            var c = resultMain[typeof(C)];
            return new Result();
        }
    }
}
