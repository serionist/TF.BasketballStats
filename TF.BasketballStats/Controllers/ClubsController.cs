using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using TF.BasketballStats.Database;

namespace TF.BasketballStats.Controllers
{
    
    [Route("api/Clubs")]
    public class ClubsController : ApiController<ClubsController>
    {
        public ClubsController(ILogger<ClubsController> log, DatabaseContext db) : base(log, db)
        {
        }
        [HttpGet("")]
        [ProducesResponseType(typeof(Club[]), 200)]
        public IActionResult GetClubs()
        {
            return Json(DB.Clubs.ToArray(), new JsonSerializerSettings() { Formatting = Formatting.Indented });
        }
      
        [HttpPost("")]
        [ProducesResponseType(typeof(Club[]), 200)]
        public IActionResult AddClub([FromBody]Club json)
        {
            DB.Clubs.Add(json);
            DB.SaveChanges();
            return GetClubs();
        }
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [HttpDelete("{id:int}")]
        public IActionResult DeleteCLub(int id)
        {
            var c = DB.Clubs.Find(id);
            if (c == null) return NotFound();
            return Json(c, new JsonSerializerSettings() {Formatting = Formatting.Indented});
        }
    }
}