using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using TNSApi.Mapping;
using TNSApi.Models;
using TNSApi.Services;

namespace TNSApi.Controllers
{
    public class AuthorizationController : ApiController
    {

        private IDatabaseServiceProvider _database;

        public AuthorizationController(IDatabaseServiceProvider database)
        {
            _database = database;
        }


        // POST: api/Wheelchair
        public IHttpActionResult Post([FromBody]User user)
        {
            // haha
            user = _database.Users.Where(x => x.Username == user.Username && x.Password == user.Password).FirstOrDefault();
            if (user == null)
            {
                return Unauthorized();
            }

            user.Token = Guid.NewGuid().ToString();

            var frontendUser = new RequestUser() { Username = user.Username, Token = user.Token, AccessLevel = user.AccessLevel, LastLogin = user.LastLogin };

            user.LastLogin = DateTime.Now;
            _database.Context.SaveChanges();

           return Ok(frontendUser);
        }
    }


    public class RequestUser
    {
        public string Username { get; set; }
        public string Token { get; set; }
        public string AccessLevel { get; set; }
        public DateTime? LastLogin { get; set; }
    }
}