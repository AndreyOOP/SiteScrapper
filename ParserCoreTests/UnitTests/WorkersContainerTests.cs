using Microsoft.VisualStudio.TestTools.UnitTesting;
using ParserCore;
using System.Collections.Generic;
using System.Linq;

namespace UnitTests.WorkersContainerUnitTest
{
    #region Test Classes
    class A { }
    class B { }
    class C { }
    class D { }
    class E { }
    class F { }
    class G { }
    class J { }
    #endregion

    [TestClass]
    public class WorkersContainerTests
    {
        #region InOut keys
        InOutKey<A, B> ab = new InOutKey<A, B>();
        InOutKey<B, C> bc = new InOutKey<B, C>();
        InOutKey<C, D> cd = new InOutKey<C, D>();
        InOutKey<C, E> ce = new InOutKey<C, E>();
        InOutKey<E, F> ef = new InOutKey<E, F>();
        InOutKey<D, G> dg = new InOutKey<D, G>();
        InOutKey<C, J> cj = new InOutKey<C, J>();
        #endregion

        [TestMethod]
        public void Last_SingleBranch_Bc()
        {
            // AB-BC
            var graph = new Dictionary<IInOutKey, object>
            {
                [ab] = null, [bc] = null
            };
            var container = new WorkersContainer(graph);

            CollectionAssert.AreEqual(new[] { bc }, container.Last.ToArray());
        }

        [TestMethod]
        public void Last_TwoSameBranches_CdCe()
        {
            // AB-BC-CD
            //     \CE
            var graph = new Dictionary<IInOutKey, object>
            {
                [ab] = null, [bc] = null, [cd] = null, [ce] = null
            };
            var container = new WorkersContainer(graph);

            CollectionAssert.AreEqual(new object[] { cd, ce }, container.Last.ToArray());
        }

        [TestMethod]
        public void Last_TwoDifferentBranches_CdEf()
        {
            // AB-BC-CD
            //     \CE-EF
            var graph = new Dictionary<IInOutKey, object>
            {
                [ab] = null, [bc] = null, [cd] = null, [ce] = null, [ef] = null
            };
            var container = new WorkersContainer(graph);

            CollectionAssert.AreEqual(new object[] { cd, ef }, container.Last.ToArray());
        }

        [TestMethod]
        public void Last_ThreeDifferentBranches_DgEfCj()
        {
            // AB-BC-CD-DG
            //     \CE-EF
            //      \CJ
            var graph = new Dictionary<IInOutKey, object>
            {
                [ab] = null, [bc] = null, [cd] = null, [dg] = null, [ce] = null, [ef] = null, [cj] = null
            };
            var container = new WorkersContainer(graph);

            CollectionAssert.AreEqual(new object[] { dg, ef, cj }, container.Last.ToArray());
        }
    }
}
