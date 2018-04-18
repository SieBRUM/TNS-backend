using System.Web.Http;
using TNSApi.Models;
using TNSApi.Services;
using System.Linq;

namespace TNSApi.Controllers
{
    public class WheelchairController : ApiController
    {
        private IDatabaseServiceProvider _database;

        public WheelchairController(IDatabaseServiceProvider database)
        {
            _database = database;
        }

        // GET: api/Wheelchair
        public IHttpActionResult Get()
        {
           var user = _database.Users.Where(x => x.UserId == 1).FirstOrDefault();

            return Ok(user);
        }

        // GET: api/Wheelchair/5
        public IHttpActionResult Get(int id)
        {
            var user = _database.Users.Where(x => x.UserId == id).FirstOrDefault();

            return Ok(user);
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
