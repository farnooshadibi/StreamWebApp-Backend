//using System;
//using System.Collections.Concurrent;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Cors;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using MyApp.Extra_Classes;
//using MyApp.Models;
//using MyApp.Models.ApiModels.Person;

//namespace MyApp.Controllers
//{
//    [Route("api/[controller]")]
//    [EnableCors("MyPolicy")]
//    [ApiController]
    
//    public class personController : ControllerBase
//    {
//        private StreamDBContext _context;

//        public personController(StreamDBContext context)
//        {
//            _context = context;
//        }

//        //private PersonDBContext db = new PersonDBContext();

//        // GET api/person
//        [HttpGet]
//        // public ActionResult<IEnumerable<string>> Get()
//        public ActionResult Get()
//        {
//            try
//            {
//                //in-memory db
//                var persons = _context.Persons.ToList();

//                var person = Container.Persons.ToList();

//                Response objResponse = new Response();
//                ResponseList responseList = new ResponseList();
//                objResponse.Data = persons;
//                objResponse.Status = true;
//                objResponse.Message = " Received successfully";

//                //responseList.reponses.Add(objResponse);


//                return Ok(objResponse);

//            }
//            catch (Exception e)
//            {
//                writeException.Write(e.Message, DateTime.Now, "Person", "Get", "Admin");
//                return this.NotFound("Dosnt Received successfully");
//            }
 
 
//        }


//        // POST api/values
//        [HttpPost("AddPost")]
//        public ActionResult AddPost([FromBody] Person person)

//        {
//            try
//            {               
//                Response objResponse = new Response();

//                //Determine the next ID
//                var newId = _context.Persons.Select(x => x.Id).Max() + 1;
//                person.Id = newId;
//                person.createAt = DateTime.Now;
//                person.userToken = Guid.NewGuid();

//                if (person.password != person.confirmPassword)
//                {
//                    return this.NotFound("Dosnt match");
//                }
//                var personObj = _context.Persons.Where(p => p.email == person.email).FirstOrDefault();
//                if( personObj != null)
//                {
//                    return this.NotFound("User is Exsist");
//                }
//                if( person.password.Length < 4)
//                {
//                    return this.NotFound("Format is incorrect");
//                }
               
//                _context.Persons.Add(person);
//                _context.SaveChanges();

//                objResponse.Data = person;
//                objResponse.Status = true;
//                objResponse.Message = " Create successfully";

                          
//                return Ok(objResponse);
//            }

//            catch (Exception e)
//            {
//                writeException.Write(e.Message, DateTime.Now, "Person", "Post", "Admin");
//                return this.NotFound("Dosnt Create successfully");
//            }


//        }

//        [HttpPost("LoginPost")]
//        public ActionResult LoginPost([FromBody] getTokenByUserLogin getToken)
//        {
//            try
//            {

//                string email = getToken.email;
//                string password = getToken.password;
//                Response objResponse = new Response();
//                Person person = new Person();

//                person = _context.Persons.Where(current => current.email == email && current.password == password).FirstOrDefault();

//                    if (person != null)
//                    {
//                        var personId = person.userToken;
//                    objResponse.Data = personId;
//                    objResponse.Status = true;
//                    objResponse.Message = "Login successfully";
//                    }
//                    else
//                    {
//                        return this.NotFound("Email or password is incorrect");
//                    }
                
//                return Ok(objResponse);

//            }
//            catch (Exception e)
//            {
//                writeException.Write(e.Message, DateTime.Now, "Person", "LoginPost", "Api");
//                return this.NotFound("Dosnt Login successfully");
//            }



//        }

//        // GET api/values/5
//        [HttpGet("{id}")]
//        public ActionResult Get(int id)
//        {
//            var person = _context.Persons.Find(id);
//            if (person == null)
//            {
//                return this.NotFound("person doesnt exist");
//            }

//            return Ok(person);
//        }
         
//        // POST api/values/5
//        [HttpPut("{id}")]
//        public ActionResult Put(int id, [FromBody] addPerson addPerson)
//        {
//            try
//            {
//                Container container = new Container();
//                Response objResponse = new Response();
//                int personId = id;
//                string email = addPerson.email;
//                string password = addPerson.password;
//                string confirmPassword = addPerson.confirmPassword;
               

//                var personObj = Container.Persons.FirstOrDefault(x => x.Id == id);
//                var person = _context.Persons.Find(id);
//                if (person == null)
//                {
//                    return this.NotFound("person doesnt exist");
//                }
//                //
//                person.password = password;
//                person.email = email;
//                person.confirmPassword = confirmPassword;


//                _context.Persons.Update(person);
//                _context.SaveChanges();

//                //_context.Entry(person).State = EntityState.Modified;
//                //_context.SaveChanges();

//                objResponse.Data = person;
//                objResponse.Status = true;
//                objResponse.Message = " Edit Successfully";


//                return Ok(objResponse);
//            }
//            catch (Exception e)
//            {
//                writeException.Write(e.Message, DateTime.Now, "Person", "Post", "Admin");
//                return this.NotFound("Dosnt Edit successfully");
//            }
//        }


//        // DELETE api/values/5
//        [HttpDelete("{id}")]
//        public ActionResult Delete(int id)
//        {
//            try
//            {
//                //var person = Container.Persons.SingleOrDefault(x => x.Id == id);
//                var person = _context.Persons.SingleOrDefault(x => x.Id == id);


//                if (person != null)
//                {
//                    _context.Persons.Remove(person);
//                    //_context.Entry(person).State = EntityState.Modified;
//                    _context.SaveChanges();

//                    //

//                    return Ok("Delete successfully");
//                }
//                else
//                {
//                    return this.NotFound("Dosnt Delete successfully");
//                }


//            }
//            catch (Exception e)
//            {
//                writeException.Write(e.Message, DateTime.Now, "Person", "Delete", "Admin");
//                return this.NotFound("Dosnt Delete successfully");
//            }
//        }
//    }
//}