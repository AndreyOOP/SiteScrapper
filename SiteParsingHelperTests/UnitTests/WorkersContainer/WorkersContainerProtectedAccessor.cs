using ParserCoreProject.Abstraction;
using ParserCoreProject.ParserCore;
using System.Collections.Generic;

namespace ParserCoreTests.UnitTests.WorkersContainer
{
    class WorkersContainerProtectedAccessor : WorkersContainer<A, B>
    {
        public Dictionary<IInOutKey, object> Workers { get; }

        public WorkersContainerProtectedAccessor()
        {
            Workers = workers;
        }
    }
}
