using System;

namespace ParserCore
{
    public interface IInOutKey
    {
        Type InType { get; }
        Type OutType { get; }
    }
}
