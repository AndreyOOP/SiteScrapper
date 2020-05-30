using ParserCoreProject.Abstraction;

namespace ParserCoreProjectTests.ParserCore
{
    class A { }
    class B { }
    class C { }

    class WorkerAB : IWorker<A, B>
    {
        public void ParseAndExecuteNext(A model) { }
    }
    class WorkerBA : IWorker<B, A>
    {
        public void ParseAndExecuteNext(B model) { }
    }
    class WorkerAC : IWorker<A, C>
    {
        public void ParseAndExecuteNext(A model) { }
    }
    class WorkerAA : IWorker<A, A>
    {
        public void ParseAndExecuteNext(A model) { }
    }
}
