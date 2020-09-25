using System.Linq;
using System.Reflection;

namespace ParserCore.Extensions
{
    public static class AttributeExtensions
    {
        /// <summary>
        /// Get custom attribute from <param name="propertyInfo" /> of type <typeparam name="T" />. Returns default if could not found the attribute 
        /// </summary>
        /// <typeparam name="T">Custom attribute type</typeparam>
        /// <param name="propertyInfo">Property metadata</param>
        /// <returns>Custom attribute data</returns>
        public static CustomAttributeData GetCustomAttributeData<T>(this PropertyInfo propertyInfo)
            => propertyInfo.CustomAttributes.FirstOrDefault(attribute => attribute.AttributeType == typeof(T));
    }
}
