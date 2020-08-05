using System;
using System.Collections.Generic;
using System.Linq;

namespace ParserCore
{
    // ToDo: 
    //   add single worker registration - ?
    //   add cycles check during registration
    //   add Register implementation, atm not required
    public class WorkersContainer : IWorkersContainer
    {
        private List<IInOutKey> last = new List<IInOutKey>();
        private Dictionary<IInOutKey, object> workers = new Dictionary<IInOutKey, object>();

        /// <inheritdoc/>
        public IEnumerable<IInOutKey> Last => last;

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

        // ToDo: 
        /// <summary>
        /// String representation of current workers graph
        /// </summary>
        public override string ToString()
        {
            return base.ToString();
        }
    }
}
