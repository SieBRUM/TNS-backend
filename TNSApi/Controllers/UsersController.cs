using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using TNSApi.Mapping;
using TNSApi.Services;

namespace TNSApi.Controllers
{
    public class UsersController : ApiController
    {
        IDatabaseServiceProvider _database;

        public UsersController(IDatabaseServiceProvider database)
        {
            _database = database;
        }

        [HttpGet]
        public IHttpActionResult GetUsers()
        {
            User user = new User();
            int authorizedMessage = (int)AuthorizationService.CheckIfAuthorized(ref user, ref _database, Request.Headers, AccessLevel.Admin);

            if (authorizedMessage == 1 || authorizedMessage == 2)
            {
                return Content(HttpStatusCode.Forbidden, "User not logged in.");
            }
            if(authorizedMessage == 3)
            {
                return Content(HttpStatusCode.Unauthorized, "User has no permission.");
            }
            if (authorizedMessage == 4)
            {
                return Content(HttpStatusCode.Forbidden, "User account is disabled.");
            }


            List<User> users = _database.Users.ToList();
            List<ListPageUser> frontendUsers = new List<ListPageUser>();

            for (int i = 0; i < users.Count; i++)
            {
                frontendUsers.Add(new ListPageUser()
                {
                    Id = users[i].Id,
                    Username = users[i].Username,
                    AccessLevel = users[i].AccessLevel,
                    LastLogin = users[i].LastLogin,
                    Created = users[i].Created,
                    IsActive = users[i].IsActive,
                });
            }

            return Ok(frontendUsers);
        }

        [HttpPost]
        public IHttpActionResult EditUser([FromBody] User user)
        {
            User requestingUser = new User();

            int authorizedMessage = (int)AuthorizationService.CheckIfAuthorized(ref requestingUser, ref _database, Request.Headers, AccessLevel.Admin);

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

            if(user.Id == requestingUser.Id)
            {
                return BadRequest("Cannot change current logged in user!");
            }


            if(user.Id == 0)
            {
                if(_database.Users.Where(x => x.Username == user.Username).FirstOrDefault() != null)
                {
                    return Content(HttpStatusCode.BadRequest, "Username already exists.");
                }
                user.Password = AuthorizationService.GetHashSha256(user.Password);
                user.Created = DateTime.Now;
                _database.Users.Add(user);
            }
            else
            {
                User changeUser = _database.Users.Where(x => x.Id == user.Id).FirstOrDefault();

                if (user.AccessLevel == "Default")
                {
                    if (changeUser.AccessLevel == "Admin")
                    {
                        if (_database.Users.Where(x => x.AccessLevel == "Admin").ToList().Count < 2)
                        {
                            return BadRequest("There has to be at least one Administrator account!");
                        }
                    }
                }

                changeUser.AccessLevel = user.AccessLevel;
                changeUser.IsActive = user.IsActive;
                if (user.Password != null)
                {
                    changeUser.Password = AuthorizationService.GetHashSha256(user.Password);
                }

                _database.Context.Entry(changeUser).State = System.Data.Entity.EntityState.Modified;
            }


            _database.Context.SaveChanges();

            List<User> users = _database.Users.ToList();
            List<ListPageUser> frontendUsers = new List<ListPageUser>();

            for (int i = 0; i < users.Count; i++)
            {
                frontendUsers.Add(new ListPageUser()
                {
                    Id = users[i].Id,
                    Username = users[i].Username,
                    AccessLevel = users[i].AccessLevel,
                    LastLogin = users[i].LastLogin,
                    Created = users[i].Created,
                    IsActive = users[i].IsActive,
                });
            }
            return Ok(frontendUsers);
        }
    }

    public class ListPageUser
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string AccessLevel { get; set; }
        public DateTime? LastLogin { get; set; }
        public DateTime Created { get; set; }
        public Boolean IsActive { get; set; }
    }
}