using ParserCoreProject.Abstraction;

namespace ParserCoreProjectTests.UnitTests.WorkerBase
{
    class A { }
    class B { }
    class C { }
    class D { }
    class E { }

    class TestWorkerBase<TIn, TOut> : WorkerBase<TIn, TOut, A, B> where TOut : new()
    {
        protected TestWorkerBase(IWorkersContainer<A, B> workersContainer) : base(workersContainer) { }

        public string Status { get; set; }

        protected override TOut ParseUnit(TIn model)
        {
            Status = $"{GetType().Name} {nameof(this.ParseUnit)} executed. {nameof(StopHere)} = {StopHere}";
            return new TOut();
        }
    }

    class ABFalse : TestWorkerBase<A, B>
    {
        public ABFalse(IWorkersContainer<A, B> workersContainer) : base(workersContainer) { }
    }

    class ABTrue : TestWorkerBase<A, B>
    {
        public ABTrue(IWorkersContainer<A, B> workersContainer) : base(workersContainer) { }
        protected override bool StopHere => true;
    }

    // In this way few paths could be executed or select someone by condition
    class ABFalseOverride : TestWorkerBase<A, B>
    {
        public ABFalseOverride(IWorkersContainer<A, B> workersContainer) : base(workersContainer) { }

        protected override void ExecuteNextWorker(B model)
        {
            workersContainer.Get<B, C>().ParseAndExecuteNext(model);
            workersContainer.Get<B, E>().ParseAndExecuteNext(model);
        }
    }

    class BCFalse : TestWorkerBase<B, C>
    {
        public BCFalse(IWorkersContainer<A, B> workersContainer) : base(workersContainer) { }
    }

    class BEFalse : TestWorkerBase<B, E>
    {
        public BEFalse(IWorkersContainer<A, B> workersContainer) : base(workersContainer) { }
    }

    class CDTrue : TestWorkerBase<C, D>
    {
        public CDTrue(IWorkersContainer<A, B> workersContainer) : base(workersContainer) { }
        protected override bool StopHere => true;
    }

    class EDTrue : TestWorkerBase<E, D>
    {
        public EDTrue(IWorkersContainer<A, B> workersContainer) : base(workersContainer) { }
        protected override bool StopHere => true;
    }
}
