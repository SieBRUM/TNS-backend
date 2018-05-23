using System;
using System.Data;
using System.Linq;
using System.Web.Http;
using TNSApi.Mapping;
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

        /// <summary>
        /// User login.
        /// Route: {host}/{alias}/api/login
        /// </summary>
        /// <param name="user">User login details given in the body of the request</param>
        /// <returns>
        /// Logged in user with accesstoken + accesslevel if successful
        /// Unauthorized if not succesful
        /// </returns>
        [Route("api/login")]
        [HttpPost]
        public IHttpActionResult Login([FromBody]User user)
        {
            string hashedPassword = AuthorizationService.GetHashSha256(user.Password);
            user = _database.Users.Where(x => x.Username == user.Username && x.Password == hashedPassword).FirstOrDefault();
            if (user == null)
            {
                return Unauthorized();
            }

            if(user.IsActive == false)
            {
                return Unauthorized();
            }

            user.Token = Guid.NewGuid().ToString();

            var frontendUser = new RequestUser() { Username = user.Username, Token = user.Token, AccessLevel = user.AccessLevel, LastLogin = user.LastLogin };

            user.LastLogin = DateTime.Now;
            _database.Context.SaveChanges();

           return Ok(frontendUser);
        }

        /// <summary>
        /// Authorizes the user when the frontend is opened. 
        /// Route: {host}/{alias}/api/authorize
        /// </summary>
        /// <param name="user">User details (username + accesstoken) given in body of request</param>
        /// <returns> 
        /// Logged in user if successful
        /// Unauthorized if not suscessful
        /// </returns>
        [HttpPost]
        public IHttpActionResult Authorize([FromBody]User user)
        {
            if (AuthorizationService.CheckIfAuthorized(ref user, ref _database, Request.Headers, AccessLevel.Default) != 0)
            {
                return Unauthorized();
            }

            var frontendUser = new RequestUser()
            {
                Username = user.Username,
                Token = user.Token,
                AccessLevel = user.AccessLevel,
                LastLogin = user.LastLogin
            };

            return Ok(frontendUser);
        }
    }

    // User class without exposed password
    public class RequestUser
    {
        public string Username { get; set; }
        public string Token { get; set; }
        public string AccessLevel { get; set; }
        public DateTime? LastLogin { get; set; }
    }
}