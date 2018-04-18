using System.Web.Http;
using TNSApi.Models;

namespace TNSApi.Controllers
{
    public class WheelchairController : ApiController
    {

        // GET: api/Wheelchair
        public IHttpActionResult Get()
        {
            TestClass test = new TestClass();
            test.name = "Siebren";
            test.age = 19;
            test.description = "Such an awesome developer, you should hire him!";
            return Ok(test);
        }

        // GET: api/Wheelchair/5
        public IHttpActionResult Get(int id)
        {
            return BadRequest("bloediebloe");
        }

        // POST: api/Wheelchair
        public IHttpActionResult Post([FromBody]string value)
        {
            return Ok();
        }

        // PUT: api/Wheelchair/5
        public IHttpActionResult Put(int id, [FromBody]string value)
        {
            return Ok();
        }

        // DELETE: api/Wheelchair/5
        public IHttpActionResult Delete(int id)
        {
            return Ok();
        }
    }
}
