using Microsoft.VisualStudio.TestTools.UnitTesting;
using ParserCore;
using System.Collections.Generic;
using System.Linq;

namespace UnitTests.ParserBase
{
    class A { }
    class B { }
    class C { }
    class D { }
    class E { }
    class F { }
    class G { }
    class J { }
    class Result { }

    class ParserBasePublic : ParserBase<A, Result>
    {
        public ParserBasePublic(IWorkersContainer workersContainer) : base(workersContainer)
        {
        }

        public IEnumerable<object> Result => result;

        public void GetFinalTOutModelsPublic(IEnumerable<object> inTypes)
        {
            GetFinalTOutModels(inTypes);
        }

        public FinalAndInputTypes SplitToFinalAndInputTypesPublic(IEnumerable<object> tOutTypes)
        {
            return SplitToFinalTypesAndInput(tOutTypes);
        }
    }

    class WorkerAB : IWorker<A, B>
    {
        public bool IsExecutable(A model) => true;

        public B Parse(A model) => new B();
    }

    class WorkerBC : IWorker<B, C>
    {
        public bool IsExecutable(B model) => true;

        public C Parse(B model) => new C();
    }

    class WorkerAD : IWorker<A, D>
    {
        public bool IsExecutable(A model) => false;

        public D Parse(A model) => new D();
    }

    [TestClass]
    public class ExecutionSample
    {
        [TestMethod]
        public void SimpleTest()
        {
            var ab = new InOutKey<A, B>();
            var bc = new InOutKey<B, C>();
            var ad = new InOutKey<A, D>();

            var graph = new Dictionary<IInOutKey, object>
            {
                [ab] = new WorkerAB(),
                [bc] = new WorkerBC(),
                [ad] = new WorkerAD()
            };
            var container = new WorkersContainer(graph);
            var parser = new ParserBasePublic(container);

            parser.GetFinalTOutModelsPublic(new object[] { new A() });

            CollectionAssert.AreEqual(new[] { typeof(C) }, parser.Result.Select(r => r.GetType()).ToArray());
        }
        
    }

    [TestClass]
    public class ParserBaseTests
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
        public void SplitToFinalAndInputTypesTest()
        {
            var a = new A();
            var b = new B();
            var c = new C();
            var d = new D();
            var e = new E();
            var f = new F();
            var g = new G();
            var j = new J();
            
            // AB-BC-CD-DG
            //     \CE-EF
            //      \CJ
            var graph = new Dictionary<IInOutKey, object>
            {
                [ab] = null,
                [bc] = null,
                [cd] = null,
                [dg] = null,
                [ce] = null,
                [ef] = null,
                [cj] = null
            };
            var container = new WorkersContainer(graph);
            var parser = new ParserBasePublic(container);
            
            var result = parser.SplitToFinalAndInputTypesPublic(new object[] { a, b, c, d, e, f, g, j});

            CollectionAssert.AreEquivalent(new object[] { g, f, j }, result.FinalTypes.ToArray());
            CollectionAssert.AreEquivalent(new object[] { a, b, c, d, e }, result.InputTypes.ToArray());
        }
    }
}
