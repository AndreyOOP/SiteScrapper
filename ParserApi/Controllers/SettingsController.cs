using ParserApi.Controllers.Models;
using ParserApi.Services;
using System.Net;
using System.Web.Http;

namespace ParserApi.Controllers
{
    public class SettingsController : ApiController
    {
        private ILoggerSettingsProvider loggerSettingsProvider;

        public SettingsController(ILoggerSettingsProvider loggerSettingsProvider)
        {
            this.loggerSettingsProvider = loggerSettingsProvider;
        }

        [HttpPost]
        [Route("api/set")]
        public HttpStatusCode SetLogLevel([FromBody]InMemoryLoggerLevel level)
        {
            loggerSettingsProvider.Update(level);
            return HttpStatusCode.OK;
        }
    }
}