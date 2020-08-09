using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyApp.Extra_Classes;
using MyApp.Models;
using MyApp.Models.ApiModels;
using MyApp.Models.ApiModels.Person;

namespace MyApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private StreamDBContext db;
        public AdminController(StreamDBContext context)
        {
            db = context;
        }

        // POST api/values
        [HttpPost("LoginPost")]
        public ActionResult LoginPost([FromBody] getTokenByUserLogin getToken)
        {
            try
            {

                string email = getToken.email;
                string password = getToken.password;
                Response objResponse = new Response();
                //Admin admin = new Admin();
                var result = db.admins.ToList();
                 var admin = db.admins.Where(current => current.UserName == email && current.Password == password).FirstOrDefault();

                if (admin != null)
                {
                   // var adminToken = admin.Token;
                    //objResponse.Data = adminToken;
                    objResponse.Status = true;
                    objResponse.Message = "Login successfully";
                }
                else
                {
                    return this.NotFound("Email or password is incorrect");
                }

                return Ok(objResponse);

            }
            catch (Exception e)
            {
                writeException.Write(e.Message, DateTime.Now, "Admin", "LoginPost", "Api");
                return this.NotFound("Dosnt Login successfully");
            }
        }
    }
}