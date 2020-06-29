using System;

namespace ParserCoreProject.Abstraction
{
    public interface IInOutKey
    {
        Type InType { get; }
        Type OutType { get; }
    }
}
