using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Http;

namespace ParserApi.FeatureTest
{
    /// <summary>
    /// Samples how to use HttpClient
    /// </summary>
    [TestClass]
    public class WebRequests
    {
        [Ignore]
        [TestMethod]
        public void GetRequestSample()
        {
            var httpClient = new HttpClient();
            var result = httpClient.GetAsync("http://911auto.com.ua").Result;
            var html = result.Content.ReadAsStringAsync().Result;
        }
    }
}
