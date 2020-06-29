namespace ParserApi.Parsers.Site911Demo.Models
{
    public class QueryString3
    {
        private string queryString;

        public string QueryString
        {
            get => queryString.Substring(8).Replace("%3D%3D", "==");
            set { queryString = value; }
        }
    }
}