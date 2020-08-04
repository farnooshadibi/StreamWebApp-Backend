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
    public class UserController : ControllerBase
    {
        private PersonDBContext db;
        public UserController(PersonDBContext context)
        {
            db = context;
        }
        [HttpGet]
        public ActionResult Get()
        {
            try
            {
                var users = db.Users.ToList();
                Response response = new Response();
                response.Data = users;
                response.Status = true;
                response.Message = " Received successfully";
                return Ok(response);

            }
            catch(Exception e)
            {
                writeException.Write(e.Message, DateTime.Now, "User", "Get", "Admin");
                return this.NotFound("Dosnt Received successfully");
            }
           
        }

        // POST api/values
        [HttpPost]
        public ActionResult Post([FromBody] User user)
        {
            try
            {
                Response response = new Response();
                
                //Determine the next ID
                var newId = db.Users.Select(x => x.Id).Max() + 1;
                user.Id = newId;
                db.Users.Add(user);
                db.SaveChanges();
                response.Data = user;
                response.Status = true;
                response.Message = " Create successfully";


                return Ok(response);
            }

            catch (Exception e)
            {
                writeException.Write(e.Message, DateTime.Now, "User", "Post", "Admin");
                return this.NotFound("Dosnt Create successfully");
            }
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            var user = db.Users.Find(id);
            if (user == null)
            {
                return this.NotFound("User doesnt exist");
            }

            return Ok(user);
        }

        // POST api/values/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] UserEdit user)
        {
            try
            {
               
                Response objResponse = new Response();                    
                var userObj = db.Users.Find(id);
                string firstName = user.firstName;
                string lastName = user.lastName;
                string mobile = user.mobile;
                if (userObj == null)
                {
                    return this.NotFound("User doesnt exist");
                }
                //
                userObj.firstName = firstName;
                userObj.lastName = lastName;
                userObj.mobile = mobile;
                db.Users.Update(userObj);
                db.SaveChanges();

                objResponse.Data = userObj;
                objResponse.Status = true;
                objResponse.Message = " Edit Successfully";

                return Ok(objResponse);
            }
            catch (Exception e)
            {
                writeException.Write(e.Message, DateTime.Now, "User", "Post", "Admin");
                return this.NotFound("Dosnt Edit successfully");
            }
        }


        // DELETE api/values/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {           
                var user = db.Users.SingleOrDefault(x => x.Id == id);


                if (user != null)
                {
                    db.Users.Remove(user);
                    //_context.Entry(person).State = EntityState.Modified;
                    db.SaveChanges();
                    return Ok("Delete successfully");
                }
                else
                {
                    return this.NotFound("Dosnt Delete successfully");
                }
            }
            catch (Exception e)
            {
                writeException.Write(e.Message, DateTime.Now, "User", "Delete", "Admin");
                return this.NotFound("Dosnt Delete successfully");
            }
        }

    }
}