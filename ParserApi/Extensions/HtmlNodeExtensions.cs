using HtmlAgilityPack;
using ParserApi.Exceptions;

namespace ParserApi.Extensions
{
    // It is possible to split them to general & site parser specific
    public static class HtmlNodeExtensions
    {
        /// <summary>
        /// In Site911 get value of cell represented by + or - gif picture
        /// </summary>
        public static bool? GetSign(this HtmlNode node)
        {
            var value = node?.FirstChild?.GetAttributeValue<string>("src", "");

            if (string.IsNullOrEmpty(value))
                return null;

            return value.EndsWith("plus.gif");
        }
    }
}