using SiteParsingHelper.Abstraction;
using System;
using System.Collections.Generic;

namespace SiteParsingHelper.Tree
{
    public class WorkUnitTree : IWorkUnitTree
    {
        private Func<IWorkUnitModel, IWorkUnitModel, ExecutionPath> selectPath;
        private Dictionary<ExecutionPath, WorkUnitTree> treeNodePaths;

        public IWorkUnit Unit { get; }

        public WorkUnitTree(IWorkUnit unit)
        {
            Unit = unit;
        }

        public Dictionary<ExecutionPath, WorkUnitTree> AddConditionalNodes(Func<IWorkUnitModel, IWorkUnitModel, ExecutionPath> selectPath, Dictionary<ExecutionPath, WorkUnitTree> treeNodePaths)
        {
            this.selectPath = selectPath;
            this.treeNodePaths = treeNodePaths;

            //ToDo: add cycle check
            return treeNodePaths;
        }

        public WorkUnitTree AddNextNode(WorkUnitTree node)
        {
            AddConditionalNodes((a, b) => ExecutionPath.Path1, new Dictionary<ExecutionPath, WorkUnitTree> { [ExecutionPath.Path1] = node });
            return node;
        }

        public WorkUnitTree NextNode(IWorkUnitModel input, IWorkUnitModel output)
        {
            if (IsLastNode())
                throw new InvalidOperationException("Last node reached");

            var path = selectPath(input, output);

            return treeNodePaths[path];
        }

        public bool IsLastNode()
        {
            return treeNodePaths == null;
        }

        public string DrawTree(string tab)
        {
            tab += "          ";
            var treeString = Unit.GetType().Name + Environment.NewLine;

            if (treeNodePaths != null)
            {
                foreach (var path in treeNodePaths)
                {
                    treeString += $"{tab}{path.Key}->{path.Value.DrawTree(tab)}";
                }
            }

            return treeString;
        }
    }
}
