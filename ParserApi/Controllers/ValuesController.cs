using System.Collections.Generic;
using System.Web.Http;

namespace ParserApi.Controllers
{
    public class ValuesController : ApiController
    {
        [HttpGet]
        [Route("api/values")]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpGet]
        [Route("api/{id}")]
        public TestModel Get([FromUri]int id)
        {
            return new TestModel
            {
                Id = 1,
                Id2 = 2
            };
        }
    }

    public class TestModel
    {
        public int Id { get; set; }
        public int Id2 { get; set; }
    }
}
