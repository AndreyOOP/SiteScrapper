using SiteParsingHelper.Tree;
using System;
using System.Collections.Generic;

namespace SiteParsingHelper.Abstraction
{
    public interface IWorkUnitTree
    {
        IWorkUnit Unit { get; }

        Dictionary<ExecutionPath, WorkUnitTree> AddConditionalNodes(Func<IWorkUnitModel, IWorkUnitModel, ExecutionPath> getPath, Dictionary<ExecutionPath, WorkUnitTree> pathToTreeNode);
        WorkUnitTree AddNextNode(WorkUnitTree node);
        bool IsLastNode();
        WorkUnitTree NextNode(IWorkUnitModel input, IWorkUnitModel output);
    }
}