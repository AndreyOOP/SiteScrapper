using Microsoft.VisualStudio.TestTools.UnitTesting;
using ParserCoreProject.ParserCore;

namespace ParserCoreProjectTests.UnitTests.InOutKeyTests
{
    class A { }
    class B { }

    [TestClass]
    public class InOutKeyTests
    {
        [TestMethod]
        public void Equals_SameKeys_True()
        {
            var key1 = new InOutKey<A, B>();
            var key2 = new InOutKey<A, B>();

            Assert.IsTrue(key1.Equals(key2));
            Assert.IsTrue(key1 == key2);
        }

        [TestMethod]
        public void Equals_SameKeys_True2()
        {
            var key1 = new InOutKey<A, B>();
            var key2 = new InOutKey<A, B>();
            var key3 = new InOutKey<A, B>();

            Assert.IsTrue(key1.Equals(key2));
            Assert.IsTrue(key2.Equals(key3));
            Assert.IsTrue(key1 == key2);
            Assert.IsTrue(key2 == key3);
        }

        [TestMethod]
        public void Equals_DifferentKeys_False()
        {
            var key1 = new InOutKey<A, B>();
            var key2 = new InOutKey<B, A>();

            Assert.IsFalse(key1.Equals(key2));
            Assert.IsFalse(key1 == key2);
        }

        [TestMethod]
        public void Equals_OneKeyIsNull_False()
        {
            var key1 = new InOutKey<A, B>();
            object key2 = null;

            Assert.IsFalse(key1.Equals(key2));
            Assert.IsFalse(key1 == key2);
        }
    }
}
