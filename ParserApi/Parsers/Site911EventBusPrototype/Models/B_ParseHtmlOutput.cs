namespace ParserApi.Parsers.Site911EventBusPrototype.Models
{
    public class B_ParseHtmlOutput
    {
        private string queryString;

        public string QueryString
        {
            get => queryString.Substring(8).Replace("%3D%3D", "==");
            set { queryString = value; }
        }
    }
}