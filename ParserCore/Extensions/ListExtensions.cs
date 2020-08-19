using System.Collections.Generic;
using System.Linq;

namespace ParserCore.Extensions
{
    public static class ListExtensions
    {
        /// <summary>
        /// Get first object of the specified type from the list of objects. Cast object to specified type
        /// </summary>
        /// <typeparam name="T">Type of object searched in the list</typeparam>
        /// <param name="list">List of objects</param>
        /// <returns>First object of the specified type</returns>
        public static T Get<T>(this IEnumerable<object> list)
            => (T)list.First(i => i.GetType() == typeof(T));
    }
}
