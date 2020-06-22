using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// todo
// Samples of dynamics usage & inheritance
namespace ParserCoreProjectTests.ParserCore
{
    interface IBb
    {
        string Do();
    }
    abstract class Bb : IBb
    {
        public string Do() => "bb";
        public abstract string D();
    }

    class Inh : Bb
    {
        public override string D() => "1";
    }
    class Inh2 : Bb 
    {
        public override string D() => "2";
    }

    [TestClass]
    public class DynamicTests
    {
        //[TestMethod]
        //public void MyTestMethod()
        //{
        //    dynamic x = new Bb();

        //    Assert.AreEqual("bb", x.Do());
        //}

        [TestMethod]
        public void MyTestMethod2()
        {
            dynamic x = new Inh();
            dynamic x2 = new Inh2();

            Assert.AreEqual("bb", x.Do());
            Assert.AreEqual("bb", x2.Do());
        }

        [TestMethod]
        public void MyTestMethod3()
        {
            dynamic x = new Inh();
            dynamic x2 = new Inh2();

            Assert.AreEqual("1", x.D());
            Assert.AreEqual("2", x2.D());
        }
    }
}
