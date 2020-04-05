using System.IO;
using System.Reflection;

namespace ParserApi.Tests.Mocks
{
    public static class StringExtensions
    {
        /// <summary>
        /// Get path to data sample files
        /// </summary>
        public static string GetFilePath(this string pathToFile)
        {
            return Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), pathToFile);
        }
    }
}
