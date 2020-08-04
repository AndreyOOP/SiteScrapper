using System;
using System.Collections.Generic;
using System.Linq;

namespace ParserCore
{
    public class WorkersContainer : IWorkersContainer
    {
        private List<IInOutKey> last = new List<IInOutKey>();
        private Dictionary<IInOutKey, object> workers = new Dictionary<IInOutKey, object>();

        /// <param name="workers">Represents workers relations</param>
        public WorkersContainer(Dictionary<IInOutKey, object> workers)
        {
            this.workers = workers;
            last = GetLastWorkers();
        }

        public void Register(IEnumerable<object> workers)
        {
            // type check

            // build graph

            last = GetLastWorkers();

            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public IEnumerable<IInOutKey> Last => last;

        public IEnumerable<KeyToWorker> Get(Type typeIn)
        {
            return workers.Where(w => w.Key.InType == typeIn)
                          .Select(x => new KeyToWorker { Key = x.Key, Worker = x.Value });
        }

        private List<IInOutKey> GetLastWorkers()
        {
            Func<Type, bool> LastType = 
                (outType) => workers.Keys.All(key => key.InType != outType);

            return workers.Keys.Where(key => LastType(key.OutType))
                               .ToList();
        }
    }
}
