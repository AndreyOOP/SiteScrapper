namespace ParserCore.Extensions
{
    public static class ObjectExtensions
    {
        /// <summary>
        /// Extension for simplified method invoke of the object, method has single input parameter
        /// </summary>
        /// <param name="obj">Object which contain method to be invoked</param>
        /// <param name="name">Method name</param>
        /// <param name="param">Method parameter</param>
        /// <returns></returns>
        public static object InvokeMethod(this object obj, string name, object param)
            => obj.GetType().GetMethod(name).Invoke(obj, new object[] { param });
    }
}
