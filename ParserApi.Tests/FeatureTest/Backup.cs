using HtmlAgilityPack;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ParserApi.Controllers;
using ParserApi.Parsers.Site911.Models;
using ParserApi.Parsers.Site911.WorkUnits;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

// store usefull things, classify later
namespace FeatureTest
{
    // Work with Path
    public static class StringExtensions
    {
        /// <summary>
        /// Get path to data sample files
        /// </summary>
        /// @"MockedDataAcceptanceTests\Data\A0httpResponse.html".GetFilePath()
        public static string GetFilePath(this string pathToFile)
        {
            return Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), pathToFile);
        }
    }

    [TestClass]
    public class ValuesControllerTest
    {
        // controller test sample
        [Ignore]
        [TestMethod]
        public void Get()
        {
            var controller = new ParsingController();

            var result = controller.ParseSingleModel("id");

            Assert.AreEqual("value1", result);
        }
    }

    [TestClass]
    public class HowToMockHttpClient
    {
        [TestMethod]
        public void Sample()
        {
            var handlerMock = new HttpMessageHandlerMock(new Dictionary<string, HttpResponseMessage>
            {
                ["http://911auto.com.ua/search/ok"] = new HttpResponseMessage { Content = new StringContent("html content"), StatusCode = HttpStatusCode.OK },
                ["http://911auto.com.ua/search/exception"] = new HttpResponseMessage { StatusCode = HttpStatusCode.InternalServerError }
            });
            var httpClient = new HttpClient(handlerMock);

            var res = httpClient.GetAsync("http://911auto.com.ua/search/ok");

            Assert.AreEqual("html content", res.Result.Content.ReadAsStringAsync().Result);
        }

        class HttpMessageHandlerMock : HttpMessageHandler
        {
            private Dictionary<string, HttpResponseMessage> responses;

            public HttpMessageHandlerMock(Dictionary<string, HttpResponseMessage> responses)
            {
                this.responses = responses;
            }

            protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
            {
                var uri = request.RequestUri.ToString();

                if (!responses.ContainsKey(uri))
                    throw new KeyNotFoundException("No response setup for provided Uri");

                return Task.FromResult(responses[uri]);
            }
        }

        // how to generate http request
        [TestMethod]
        public void SampleOfDataGen()
        {
            var worker = new HtmlStep1ToPrimaryResultStep2();

            var htmlDoc = new HtmlDocument();
            var node = htmlDoc.CreateElement("table");
            node.Attributes.Add("id", "product_tbl");
            node.Attributes.Add("class", "product_list");
            htmlDoc.DocumentNode.AppendChild(node);

            var html = htmlDoc.DocumentNode.OuterHtml;
            var res = worker.IsExecutable(new HtmlStep1 { Html = html });

            Assert.IsTrue(res);
        }
    }

    // simplified Html node creation
    public static class HtmlExtensions
    {
        public static HtmlNode Create(this HtmlDocument htmlDoc, string name, Dictionary<string, string> attr)
        {
            var child = htmlDoc.CreateElement(name);

            foreach (var i in attr)
            {
                child.Attributes.Add(i.Key, i.Value);
            }
            return child;
        }
    }

}
