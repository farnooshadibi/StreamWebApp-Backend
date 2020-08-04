using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyApp.Extra_Classes;
using MyApp.Models;
using MyApp.Models.ApiModels;

namespace MyApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {

        private PersonDBContext db;
        public CustomerController(PersonDBContext context)
        {
            db = context;
        }

        [HttpGet]
        public ActionResult Get()
        {
            try
            {
                var customers = db.customers.ToList();
                Response response = new Response();
                response.Data = customers;
                response.Status = true;
                response.Message = "Received successfully";
                return Ok(response);

            }
            catch (Exception e)
            {
                writeException.Write(e.Message, DateTime.Now, "Customer", "Get", "Admin");
                return this.NotFound("Dosnt Received successfully");
            }

        }

        // POST api/values
        [HttpPost]
        public ActionResult Post([FromBody] Customer customer)
        {
            try
            {
                Response response = new Response();

                //Determine the next ID
                var newId = db.customers.Select(x => x.Id).Max() + 1;
                customer.Id = newId;
                //customer.StreamKey = Guid.NewGuid();
                customer.StreamKey = newId;
                customer.Url = string.Format("http://185.194.76.218:8080/live/{0}.m3u8", customer.StreamKey);

                db.customers.Add(customer);
                db.SaveChanges();
                response.Data = customer;
                response.Status = true;
                response.Message = " Create successfully";


                return Ok(response);
            }

            catch (Exception e)
            {
                writeException.Write(e.Message, DateTime.Now, "Customer", "Post", "Admin");
                return this.NotFound("Dosnt Create successfully");
            }
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            try
            {
                var customer = db.customers.Find(id);
                if (customer == null)
                {
                    return this.NotFound("person doesnt exist");
                }
                else
                {

                    return Ok(customer);
                }
            }
            catch (Exception e)
            {
                writeException.Write(e.Message, DateTime.Now, "Customer", "Get", "Admin");
                return this.NotFound("Dosnt Get Customer successfully");
            }


        }
    }
}