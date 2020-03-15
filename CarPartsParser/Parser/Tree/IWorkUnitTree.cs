using CarPartsParser.Abstraction.Models;
using CarPartsParser.SiteParsers.Abstraction.WorkUnits;
using System;
using System.Collections.Generic;

namespace CarPartsParser.Parser.Tree
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