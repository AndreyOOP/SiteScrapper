using ParserCoreProject.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParserCoreProject.ParserCore
{
    public class WorkersChain
    {
        public IWorker3 First { get; set; }

        public IEnumerable<IWorker3> Last { get; set; }

        public IEnumerable<Tuple<Type, IWorker3>> Get<TIn>()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Tuple<Type, IWorker3>> Get(Type t)
        {
            throw new NotImplementedException();
        }
    }
}
