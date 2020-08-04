using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParserCore
{
    public class WorkersContainer : IWorkersContainer
    {
        private Dictionary<IInOutKey, object> workers = new Dictionary<IInOutKey, object>();

        public WorkersContainer()
        {
        }

        public WorkersContainer(Dictionary<IInOutKey, object> workers) // just for quick test
        {
            this.workers = workers;
        }

        public void Register(IEnumerable<KeyToWorker> workers)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IInOutKey> Last {
            get 
            {
                var lst = new List<IInOutKey>();
                foreach(var a in workers.Keys)
                {
                    if(workers.Keys.All(k => k.InType != a.OutType))
                        lst.Add(a);
                }
                return lst;
            }
        }

        public IEnumerable<KeyToWorker> Get(Type typeIn)
        {
            return workers.Where(w => w.Key.InType == typeIn)
                          .Select(x => new KeyToWorker { Key = x.Key, Worker = x.Value });
        }
    }
}
