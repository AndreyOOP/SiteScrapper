using ParserCoreProject.Abstraction;
using System;

namespace ParserCoreProjectTests.UnitTests.WorkerBase
{
    class A { }
    class B { }
    class C { }
    class D { }
    class E { }
    class Result 
    {
        public string TestStatus { get; set; }
    }

    class TestWorkerBase<TIn, TOut> : WorkerBase<TIn, TOut, A, B, Result> where TOut : new()
    {
        protected TestWorkerBase(IWorkerSharedServices<A, B, Result> sharedServices) : base(sharedServices) { }

        public string Status { get; set; }

        protected override TOut ParseUnit(TIn model)
        {
            Status = $"{GetType().Name} {nameof(this.ParseUnit)} executed. {nameof(StopHere)} = {StopHere}";
            return new TOut();
        }
    }

    class ABFalse : TestWorkerBase<A, B>
    {
        public ABFalse(IWorkerSharedServices<A, B, Result> sharedServices) : base(sharedServices) { }
    }

    class ABTrue : TestWorkerBase<A, B>
    {
        public ABTrue(IWorkerSharedServices<A, B, Result> sharedServices) : base(sharedServices) { }
        
        protected override bool StopHere => true;

        protected override B ParseUnit(A model)
        {
            sharedServices.Result.TestStatus = $"Set value in {nameof(ABFalse)}";
            return base.ParseUnit(model);
        }
    }

    // In this way few paths could be executed or select someone by condition
    class ABFalseOverride : TestWorkerBase<A, B>
    {
        public ABFalseOverride(IWorkerSharedServices<A, B, Result> sharedServices) : base(sharedServices) { }

        protected override void ExecuteNextWorker(B model)
        {
            var workersContainer = sharedServices.WorkersContainer;

            workersContainer.Get<B, C>().ParseAndExecuteNext(model);
            workersContainer.Get<B, E>().ParseAndExecuteNext(model);
        }
    }

    class BCFalse : TestWorkerBase<B, C>
    {
        public BCFalse(IWorkerSharedServices<A, B, Result> sharedServices) : base(sharedServices) { }
    }

    class BEFalse : TestWorkerBase<B, E>
    {
        public BEFalse(IWorkerSharedServices<A, B, Result> sharedServices) : base(sharedServices) { }
    }

    class CDTrue : TestWorkerBase<C, D>
    {
        public CDTrue(IWorkerSharedServices<A, B, Result> sharedServices) : base(sharedServices) { }
        protected override bool StopHere => true;
    }

    class EDTrue : TestWorkerBase<E, D>
    {
        public EDTrue(IWorkerSharedServices<A, B, Result> sharedServices) : base(sharedServices) { }
        protected override bool StopHere => true;
    }

    class BCException : TestWorkerBase<B, C>
    {
        public BCException(IWorkerSharedServices<A, B, Result> sharedServices) : base(sharedServices) { }

        protected override void ExecuteNextWorker(C model)
        {
            throw new Exception("Test exception");
        }
    }
}
