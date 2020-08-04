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
    public class ChartController : ControllerBase
    {
        private PersonDBContext _context;

        public ChartController(PersonDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        // public ActionResult<IEnumerable<string>> Get()
        public ActionResult Get()
        {
            try
            {

                //in-memory db
                var points = _context.lines.ToList();
                
                Response objResponse = new Response();
                lineDto lines = new lineDto();
                var l = new List<lineDto>();
                var listItem = new List<Line>();
                listItem.Add(new Line { Id = 1, name = "A", x = "1", y = "2" });
                listItem.Add(new Line { Id = 2, name = "B", x = "1", y = "8" });


            var list = new List<Line>();
                var list1 = _context.lines.Where(c => c.name == "A").ToList();
                var list2 = _context.lines.Where(c => c.name == "B").ToList();

                list.Add(new Line { Id=1 , name="A" , x="1", y="2"} );
                list.Add(new Line { Id = 1, name = "B", x = "1", y = "2" });
                l.Add(new lineDto {  lines = list1 });
                l.Add(new lineDto {  lines = list2 });
     

                ResponseList responseList = new ResponseList();
                objResponse.Data = l;
                objResponse.Status = true;
                objResponse.Message = " Received successfully";

                //responseList.reponses.Add(objResponse);


                return Ok(objResponse);

            }
            catch (Exception e)
            {
                writeException.Write(e.Message, DateTime.Now, "Chart", "Get", "Admin");
                return this.NotFound("Dosnt Received successfully");
            }


        }


    }
}