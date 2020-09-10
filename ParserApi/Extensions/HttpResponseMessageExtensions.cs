using System.Net;
using System.Net.Http;
using System.Web;

namespace ParserApi.Extensions
{
    // ToDo: better rename | split responsibility of getting content & check response status
    public static class HttpResponseMessageExtensions
    {
        /// <summary>
        /// Extracts string content from <see cref="HttpResponseMessage"/>
        /// </summary>
        /// <exception cref="HttpException">Thrown if response status code is not 200 OK</exception>
        public static string GetContent(this HttpResponseMessage httpResponse)
        {
            if (httpResponse.StatusCode  == HttpStatusCode.OK)
            {
                return httpResponse.Content.ReadAsStringAsync().Result;
            }
            throw new HttpException(Resource.HttpStatusCodeNotOk);
        }
    }
}