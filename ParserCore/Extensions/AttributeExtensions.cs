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

        // ToDo: get argument by type (if single) if not use position 
        /// <summary>
        /// Get value of constructor argument of type <typeparam name="T" /> at <param name="position" />
        /// </summary>
        /// <typeparam name="T">Type of the constructor argument</typeparam>
        /// <param name="data">Custom attribute metadata</param>
        /// <param name="position">Optional, position of the constructor argument</param>
        /// <returns>Value of the constructor argument</returns>
        public static T GetConstructorArgument<T>(this CustomAttributeData data, int position = 0)
            => (T)data.ConstructorArguments[position].Value;
    }
}
