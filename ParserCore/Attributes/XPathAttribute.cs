using System;
using HtmlAgilityPack;

namespace ParserCore.Attributes
{
    /// <summary>
    /// Required to map property value and <see cref="HtmlNode"/> value found by XPath
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class XPathAttribute : Attribute
    {
        private string xPath;

        public XPathAttribute(string xPath)
        {
            this.xPath = xPath;
        }
    }
}
