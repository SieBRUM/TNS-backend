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
using MoreLinq;

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

        // GET: api/Wheelchair
        public IHttpActionResult Get(int Id)
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

            var wheelchair = _database.Wheelchairs.Where(x => x.Id == Id).FirstOrDefault();
            if(wheelchair == null || wheelchair.Id == 0)
            {
                return NotFound();
            }

            wheelchair.Articles = _database.WheelchairArticles.Where(x => x.WheelchairId == wheelchair.Id).ToList();
            wheelchair.Frontwheels = _database.WheelchairFrontwheels.Where(x => x.WheelchairId == wheelchair.Id).ToList();
            wheelchair.Hoops = _database.WheelchairHoops.Where(x => x.WheelchairId == wheelchair.Id).ToList();
            wheelchair.Tires = _database.WheelchairTires.Where(x => x.WheelchairId == wheelchair.Id).ToList();
            wheelchair.Wheelprotectors = _database.WheelchairWheelprotectors.Where(x => x.WheelchairId == wheelchair.Id).ToList();
            wheelchair.Wheels = _database.WheelchairWheels.Where(x => x.WheelchairId == wheelchair.Id).ToList();

            return Ok(wheelchair);
        }

        // POST: api/Wheelchair
        public IHttpActionResult Post([FromBody] Wheelchair wheelchair)
        {
            Wheelchair newWheelchair = new Wheelchair();

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

            if(wheelchair.FootplateWidth == 0 || wheelchair.FrameLength == 0 || wheelchair.LowerLegWidth == 0 || wheelchair.SeatDepth == 0 
                || wheelchair.SeatHeightBack == 0 || wheelchair.SeatHeightFront == 0 || wheelchair.SeatWidth == 0)
            {
                return BadRequest("Incorrect wheelchair sizes!");
            }

            if (wheelchair.Articles == null)
            {
                return BadRequest("Wheelchair must have atleast one article!");
            }

            if(wheelchair.CustomerId == 0)
            {
                return BadRequest("Wheelchair must have a customer bound!");
            }

            if(wheelchair.Frontwheels ==  null)
            {
                return BadRequest("Wheelchair must have atleast one frontwheel set!");
            }

            if (wheelchair.Hoops == null)
            {
                return BadRequest("Wheelchair must have atleast one hoop set!");
            }

            if (wheelchair.Tires == null)
            {
                return BadRequest("Wheelchair must have atleast one tire set!");
            }

            if (wheelchair.Wheels == null)
            {
                return BadRequest("Wheelchair must have atleast one wheel set!");
            }

            if (wheelchair.Color == null)
            {
                return BadRequest("Wheelchair must have a color!");
            }

            if (wheelchair.OldId == 0)
            {
                wheelchair.DateOfMeasurement = DateTime.Now;
                wheelchair.Dealer = "TNS Rijen";
                wheelchair.UserId = user.Id;

                _database.Wheelchairs.Add(wheelchair);
            }
            else
            {
                wheelchair.User = user;
                wheelchair.UserId = user.Id;
                wheelchair.RalId = wheelchair.Color.Id;

                var articles = _database.WheelchairArticles.Where(x => x.WheelchairId == wheelchair.OldId);
                var frontwheels = _database.WheelchairFrontwheels.Where(x => x.WheelchairId == wheelchair.OldId);
                var hoops = _database.WheelchairHoops.Where(x => x.WheelchairId == wheelchair.OldId);
                var tires = _database.WheelchairTires.Where(x => x.WheelchairId == wheelchair.OldId);
                var wps = _database.WheelchairWheelprotectors.Where(x => x.WheelchairId == wheelchair.OldId);
                var wheels = _database.WheelchairWheels.Where(x => x.WheelchairId == wheelchair.OldId);
                if (articles != null)
                {
                    _database.WheelchairArticles.RemoveRange(articles);
                }
                if(frontwheels != null)
                {
                    _database.WheelchairFrontwheels.RemoveRange(frontwheels);
                }
                if(hoops != null)
                {
                    _database.WheelchairHoops.RemoveRange(hoops);
                }
                if(tires != null)
                {
                    _database.WheelchairTires.RemoveRange(tires);
                }
                if(wps != null)
                {
                    _database.WheelchairWheelprotectors.RemoveRange(wps);
                }
                if(wheels != null)
                {
                    _database.WheelchairWheels.RemoveRange(wheels);
                }

                foreach (var item in wheelchair.Articles)
                {
                    _database.Context.Entry(item).State = System.Data.Entity.EntityState.Detached;
                    item.WheelchairId = 0;
                }
                foreach (var item in wheelchair.Frontwheels)
                {
                    _database.Context.Entry(item).State = System.Data.Entity.EntityState.Detached;
                    item.WheelchairId = 0;
                }
                foreach (var item in wheelchair.Hoops)
                {
                    _database.Context.Entry(item).State = System.Data.Entity.EntityState.Detached;
                    item.WheelchairId = 0;
                }
                foreach (var item in wheelchair.Tires)
                {
                    _database.Context.Entry(item).State = System.Data.Entity.EntityState.Detached;
                    item.WheelchairId = 0;
                }
                foreach (var item in wheelchair.Wheelprotectors)
                {
                    _database.Context.Entry(item).State = System.Data.Entity.EntityState.Detached;
                    item.WheelchairId = 0;
                }
                foreach (var item in wheelchair.Wheels)
                {
                    _database.Context.Entry(item).State = System.Data.Entity.EntityState.Detached;
                    item.WheelchairId = 0;
                }
                _database.Context.SaveChanges();

                _database.Wheelchairs.Remove(_database.Wheelchairs.Where(x => x.Id == wheelchair.OldId).FirstOrDefault());
                _database.Context.SaveChanges();
                newWheelchair = OverwriteWheelchairData(wheelchair);

                _database.Wheelchairs.Add(newWheelchair);
            }

            _database.Context.SaveChanges();

            if(newWheelchair.Id == 0)
            {
                return Ok(wheelchair);
            }
            else
            {
                return Ok(newWheelchair);
            }
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

            products.RalColors = products.RalColors.DistinctBy(i => i.HexColorCode).ToList();


            return Ok(products);
        }

        private Wheelchair OverwriteWheelchairData(Wheelchair wheelchair)
        {

            Wheelchair newWheelchair = new Wheelchair();
            newWheelchair.Addition = wheelchair.Addition;
            newWheelchair.AdditionId = wheelchair.AdditionId;
            List<WheelchairArticle> newArticles = new List<WheelchairArticle>();
            List<WheelchairFrontwheel> newFrontwheels = new List<WheelchairFrontwheel>();
            List<WheelchairHoop> newHoops = new List<WheelchairHoop>();
            List<WheelchairTire> newTires = new List<WheelchairTire>();
            List<WheelchairWheel> newWheels = new List<WheelchairWheel>();
            List<WheelchairWheelprotector> newWP = new List<WheelchairWheelprotector>();
            foreach (var item in wheelchair.Articles)
            {
                newArticles.Add(new WheelchairArticle()
                {
                    Addition = item.Addition,
                    AdditionId = item.AdditionId,
                    ArticleId = item.ArticleId
                });
            }
            foreach (var item in wheelchair.Frontwheels)
            {
                newFrontwheels.Add(new WheelchairFrontwheel()
                {
                    Addition = item.Addition,
                    AdditionId = item.AdditionId,
                    FrontWheelId = item.FrontWheelId
                });
            }
            foreach (var item in wheelchair.Hoops)
            {
                newHoops.Add(new WheelchairHoop()
                {
                    Addition = item.Addition,
                    AdditionId = item.AdditionId,
                    HoopId = item.HoopId
                });
            }
            foreach (var item in wheelchair.Tires)
            {
                newTires.Add(new WheelchairTire()
                {
                    Addition = item.Addition,
                    AdditionId = item.AdditionId,
                    TireId = item.TireId
                });
            }
            foreach (var item in wheelchair.Wheels)
            {
                newWheels.Add(new WheelchairWheel()
                {
                    Addition = item.Addition,
                    AdditionId = item.AdditionId,
                    WheelId = item.WheelId
                });
            }
            foreach (var item in wheelchair.Wheelprotectors)
            {
                newWP.Add(new WheelchairWheelprotector()
                {
                    Addition = item.Addition,
                    AdditionId = item.AdditionId,
                    WheelprotectorId = item.WheelprotectorId
                });
            }

            newWheelchair.Articles = newArticles;
            newWheelchair.BackrestHeight = wheelchair.BackrestHeight;
            newWheelchair.BalancePoint = wheelchair.BalancePoint;
            newWheelchair.Color = wheelchair.Color;
            newWheelchair.CustomerId = wheelchair.CustomerId;
            newWheelchair.DateOfMeasurement = wheelchair.DateOfMeasurement;
            newWheelchair.Dealer = wheelchair.Dealer;
            newWheelchair.FootplateWidth = wheelchair.FootplateWidth;
            newWheelchair.FrameLength = wheelchair.FrameLength;
            newWheelchair.Frontwheels = wheelchair.Frontwheels;
            newWheelchair.Hoops = newHoops;
            newWheelchair.LowerLegWidth = wheelchair.LowerLegWidth;
            newWheelchair.OrderDate = wheelchair.OrderDate;
            newWheelchair.RalId = wheelchair.RalId;
            newWheelchair.SeatDepth = wheelchair.SeatDepth;
            newWheelchair.SeatHeightBack = wheelchair.SeatHeightBack;
            newWheelchair.SeatHeightFront = wheelchair.SeatHeightFront;
            newWheelchair.SeatWidth = wheelchair.SeatWidth;
            newWheelchair.SerialNumber = wheelchair.SerialNumber;
            newWheelchair.Tires = newTires;
            newWheelchair.User = wheelchair.User;
            newWheelchair.UserId = wheelchair.UserId;
            newWheelchair.Wheelprotectors = newWP;
            newWheelchair.Wheels = newWheels;

            return newWheelchair;
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
