using System.Web.Http;
using TNSApi.Models;
using TNSApi.Services;
using System.Linq;
using TNSApi.Mapping;
using System.Net;
using System.Collections;
using System.Collections.Generic;
using System;
using TNSApi.Mapping.Link_tables;

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
            User user = new User();
            int authorizedMessage = (int)AuthorizationService.CheckIfAuthorized(ref user, ref _database, Request.Headers, AccessLevel.Default);


            if (authorizedMessage == 1 || authorizedMessage == 2)
            {
                return Content(HttpStatusCode.Forbidden, "User not logged in.");
            }
            if (authorizedMessage == 3)
            {
                return Content(HttpStatusCode.Unauthorized, "User has no permission.");
            }
            if (authorizedMessage == 4)
            {
                return Content(HttpStatusCode.Forbidden, "User account is disabled.");
            }

            var wheelchairs = _database.Wheelchairs.ToList();

            return Ok(wheelchairs);
        }

        // POST: api/Wheelchair
        public IHttpActionResult Post([FromBody] Wheelchair wheelchair)
        {
            User user = new User();
            int authorizedMessage = (int)AuthorizationService.CheckIfAuthorized(ref user, ref _database, Request.Headers, AccessLevel.Admin);

            if (authorizedMessage == 1 || authorizedMessage == 2)
            {
                return Content(HttpStatusCode.Forbidden, "User not logged in.");
            }
            if (authorizedMessage == 3)
            {
                return Content(HttpStatusCode.Unauthorized, "User has no permission.");
            }
            if (authorizedMessage == 4)
            {
                return Content(HttpStatusCode.Forbidden, "User account is disabled.");
            }

            if (wheelchair.Id == 0)
            {
                wheelchair.CustomerId = 2;
                wheelchair.DateOfMeasurement = DateTime.Now;
                wheelchair.Dealer = "Dirk";
                wheelchair.UserId = user.Id;

                _database.Wheelchairs.Add(wheelchair);
                _database.Context.SaveChanges();

                
            }
            else
            {

                foreach (var item in _database.WheelchairArticles.Where(x => x.WheelchairId == wheelchair.Id))
                {
                    _database.WheelchairArticles.Remove(item);
                }
            }
            _database.Context.SaveChanges();

            return Ok(wheelchair);
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

        [Route("api/wheelchair/products")]
        [HttpGet]
        public IHttpActionResult GetProducts()
        {
            User user = new User();
            int authorizedMessage = (int)AuthorizationService.CheckIfAuthorized(ref user, ref _database, Request.Headers, AccessLevel.Default);

            if (authorizedMessage == 1 || authorizedMessage == 2)
            {
                return Content(HttpStatusCode.Forbidden, "User not logged in.");
            }
            if (authorizedMessage == 3)
            {
                return Content(HttpStatusCode.Unauthorized, "User has no permission.");
            }
            if (authorizedMessage == 4)
            {
                return Content(HttpStatusCode.Forbidden, "User account is disabled.");
            }

            Products products = new Products();
            products.Articles = _database.Articles.ToList();
            products.FrontWheels = _database.Frontwheels.ToList();
            products.Hoops = _database.Hoops.ToList();
            products.Additions = _database.Additions.ToList();
            products.Wheel = _database.Wheels.ToList();
            products.WheelProtector = _database.Wheelprotectors.ToList();
            products.RalColors = _database.RalColors.ToList();
            products.Tires = _database.Tires.ToList();


            return Ok(products);
        }
    }

    public class Products
    {
        public ICollection<Article> Articles { get; set; }
        public ICollection<Frontwheel> FrontWheels { get; set; }
        public ICollection<Hoop> Hoops { get; set; }
        public ICollection<Addition> Additions { get; set; }
        public ICollection<Wheel> Wheel { get; set; }
        public ICollection<Wheelprotector> WheelProtector { get; set; }
        public ICollection<Tire> Tires { get; set; }
        public ICollection<RalColor> RalColors { get; set; }
    }
}
