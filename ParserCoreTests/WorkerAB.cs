using ParserCore;

namespace ParserCoreTests
{
    public class WorkerAB : IWorker<A, B>
    {
        public B Parse(A model)
        {
            return new B();
        }

        public bool ToExecute(A model) => true;
    }

    public class WorkerBC : IWorker<B, C>
    {
        public C Parse(B model)
        {
            return new C();
        }

        public bool ToExecute(B model) => true;
    }

    public class A { }
    public class B { }
    public class C { }
    public class Result { }
}
