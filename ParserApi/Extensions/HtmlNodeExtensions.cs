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
        /// <exception cref="ParsingException">Thrown if src attribute is empty or not found</exception>
        public static bool GetSign(this HtmlNode node)
        {
            var value = node?.FirstChild?.GetAttributeValue<string>("src", "");

            if (string.IsNullOrEmpty(value))
                throw new ParsingException("Value of 'src' attribute not found");

            return value.EndsWith("plus.gif");
        }
    }
}