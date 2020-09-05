namespace ParserApi.Parsers.Site911.Models
{
    public class QueryStringStep2
    {
        private string queryStringValue;

        public string Value => queryStringValue;

        public QueryStringStep2(string completeQueryString)
        {
            queryStringValue = $"=={completeQueryString.Substring(14)}";
        }
    }
}