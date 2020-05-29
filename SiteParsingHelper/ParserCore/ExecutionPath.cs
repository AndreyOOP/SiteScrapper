using SiteParsingHelper.Event.Abstraction;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ParserCoreProject.ParserCore
{
    // ToDo: add unit tests
    public class ExecutionPath : IExecutionPath
    {
        private int order = 0;
        private Dictionary<string, int> executedUnits { get; set; } = new Dictionary<string, int>();

        public void Add(string name)
        {
            executedUnits.Add(name, order++);
        }

        public bool AlreadyExecuted(string name)
        {
            return executedUnits.ContainsKey(name);
        }

        public override string ToString()
        {
            var path = new StringBuilder();

            foreach (var part in executedUnits.OrderBy(v => v.Value))
            {
                path.Append($"{part.Key} > ");
            }

            return path.Remove(path.Length - 3, 3).ToString();
        }
    }
}
