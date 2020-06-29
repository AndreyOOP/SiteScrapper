using System.Collections.Generic;

namespace ParserCoreProject.Abstraction
{
    public interface IEdgeWorkers
    {
        IInOutKey FirstWorker { get; }

        IEnumerable<IInOutKey> LastWorkers { get; }
    }
}
