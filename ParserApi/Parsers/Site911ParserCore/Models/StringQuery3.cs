namespace ParserApi.Parsers.Site911ParserCore.Models
{
    public class StringQuery3
    {
        private string queryString;

        public string QueryString
        {
            get => queryString.Substring(8).Replace("%3D%3D", "==");
            set { queryString = value; }
        }
    }
}