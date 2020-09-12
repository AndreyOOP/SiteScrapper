using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Net.Http;

namespace FeatureTest
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

        [Ignore]
        [TestMethod]
        public void PostAsync_FormUrlEncodedContent()
        {
            var httpClient = new HttpClient();

            var body = new FormUrlEncodedContent(new[]{
                new KeyValuePair<string, string>("svc", "1"),
                new KeyValuePair<string, string>("q", "==qn2GtoXydrn1dDZvwDXvMC") // replace %3D%3D to ==  %3D%3Dqn2GtoXydrn1dDZvwDXvMC   ==qn2GtoXydrn1dDZvwDXvMC
            });

            var response = httpClient.PostAsync("https://911auto.com.ua", body).Result; // post to https not http
            var content = response.Content.ReadAsStringAsync().Result;
        }
    }
}
