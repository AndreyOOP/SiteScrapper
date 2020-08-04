using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParserCore
{
    public interface IWorkersContainer
    {
        /// <summary>
        /// Build graph of related workers
        /// </summary>
        void Register(IEnumerable<KeyToWorker> workers);

        /// <summary>
        /// List of last workers where parsing have to stop
        /// </summary>
        IEnumerable<IInOutKey> Last { get; }

        IEnumerable<KeyToWorker> Get(Type typeIn);
    }

    public class KeyToWorker
    {
        public IInOutKey Key { get; set; }
        public object Worker { get; set; }
    }

    public class TypeToModel
    {
        public Type Type { get; set; }
        public object Model { get; set; }
    }
}
