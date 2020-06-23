using SiteParsingHelper.Event.Abstraction;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("ParserCoreProjectTests")]
namespace ParserCoreProject.ParserCore
{
    internal class ExecutionPath : IExecutionPath
    {
        private int order = 0;
        private Dictionary<string, int> executedUnits { get; set; } = new Dictionary<string, int>();

        /// <exception cref="System.ArgumentException">Throwed if same record adds twice</exception>
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

            return path.Length == 0 ? "" : path.Remove(path.Length - 3, 3).ToString();
        }
    }
}
