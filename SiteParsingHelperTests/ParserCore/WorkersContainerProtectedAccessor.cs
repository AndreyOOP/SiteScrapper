using ParserCoreProject.ParserCore;
using ParserCoreProjectTests.ParserCore;
using System;
using System.Collections.Generic;

namespace ParserCoreTests.ParserCore
{
    class WorkersContainerProtectedAccessor : WorkersContainer<A, B>
    {
        public Dictionary<Tuple<Type, Type>, object> Workers { get; }

        public WorkersContainerProtectedAccessor()
        {
            Workers = workers;
        }
    }
}
