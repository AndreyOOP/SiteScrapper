using SiteParsingHelper.Abstraction;

namespace ParserApi.Parsers._911Site.Models
{
    public class B0parseHtmlResult : IWorkUnitModel
    {
        private string queryString;

        public string QueryString
        {
            get => queryString.Substring(8).Replace("%3D%3D", "==");
            set { queryString = value; }
        }
    }
}