using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using TNSApi.Mapping;
using TNSApi.Services;

namespace TNSApi.Controllers
{
    public class CustomersController : ApiController
    {
        IDatabaseServiceProvider _database;

        public CustomersController(IDatabaseServiceProvider database)
        {
            _database = database;
        }

        [HttpGet]
        public IHttpActionResult GetCustomers()
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

            List<Customer> customers = _database.Customers.ToList();

            return Ok(customers);
        }

        [HttpPost]
        public IHttpActionResult EditCustomer([FromBody]Customer customer)
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

            if(customer.Id == 0)
            {
                _database.Customers.Add(customer);
            }
            else
            {
                Customer changeCustomer = _database.Customers.Where(x => x.Id == customer.Id).FirstOrDefault();
                changeCustomer.Address = customer.Address;
                changeCustomer.City = customer.City;
                changeCustomer.Email = customer.City;
                changeCustomer.Name = customer.Name;
                changeCustomer.PhoneNumber = customer.PhoneNumber;
                changeCustomer.Zipcode = customer.Zipcode;

                _database.Context.Entry(changeCustomer).State = System.Data.Entity.EntityState.Modified;
            }

            _database.Context.SaveChanges();
            List<Customer> customers = _database.Customers.ToList();

            return Ok(customers);
        }
    }
}