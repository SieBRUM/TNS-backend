using System.Web.Http;
using TNSApi.Models;
using TNSApi.Services;
using System.Linq;
using TNSApi.Mapping;
using System.Net;

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
            var wheelchairs = _database.Wheelchairs.ToList();

            return Ok(wheelchairs);
        }

        // GET: api/Wheelchair/5
        public IHttpActionResult Get(int id)
        {
            var wheelchair = _database.Wheelchairs.Where(x => x.Id == id).FirstOrDefault();

            if(wheelchair == null)
            {
                return Content(HttpStatusCode.NotFound, "Could not find wheelchair with id: " + id);
            }

            return Ok(wheelchair);
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
