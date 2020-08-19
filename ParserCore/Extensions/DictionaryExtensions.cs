using System;
using System.Collections.Generic;

namespace ParserCore.Extensions
{
    public static class DictionaryExtensions
    {
        public static T Get<T>(this Dictionary<Type, object> dictionary)
            => (T)dictionary[typeof(T)];
    }
}
