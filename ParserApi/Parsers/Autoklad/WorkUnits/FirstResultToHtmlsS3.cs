using ParserApi.Extensions;
using ParserApi.Parsers.Autoklad.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace ParserApi.Parsers.Autoklad.WorkUnits
{
    public class FirstResultToHtmlsS3 : AutokladWorkerBase<FirstResultAK, HtmlsS3>
    {
        private readonly HttpClient httpClient;

        public FirstResultToHtmlsS3(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public override bool IsExecutable(FirstResultAK model)
            => model.FirstResult != null;

        public override HtmlsS3 Parse(FirstResultAK model)
        {
            var htmls = new HtmlsS3 { Htmls = new Dictionary<string, string>() };
            foreach(var link in model.FirstResult)
            {
                try
                {
                    var searchUri = new Uri(AutokladBase, link.PartBrandLink);
                    var response = httpClient.GetAsync(searchUri).Result;
                    htmls.Htmls[link.PartBrandLink] = response.GetContent();
                }
                catch (Exception)
                {
                    htmls.Htmls[link.PartBrandLink] = null;
                }
            }
            return htmls;
        }
    }
}