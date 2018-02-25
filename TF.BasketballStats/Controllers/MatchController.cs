using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using TF.BasketballStats.Database;
using TF.BasketballStats.Models.Matches;

namespace TF.BasketballStats.Controllers
{
    [Route("api/matches")]
    public class MatchController:ApiController<MatchController>
    {
        public MatchController(ILogger<MatchController> log, DatabaseContext db) : base(log, db)
        {
        }
        [HttpGet("")]
        [ProducesResponseType(typeof(Match[]), 200)]
        public IActionResult GetMatches()
        {
            return Json(DB.Matches.Include(e => e.OpponentClub), new JsonSerializerSettings(){Formatting = Formatting.Indented});
        }
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(Match[]), 200)]
        [ProducesResponseType(404)]
        public IActionResult GetMatch(int id)
        {
            var model = DB.Matches.Include(e => e.OpponentClub).FirstOrDefault(e => e.Id == id);
            if (model == null)
                return NotFound();
            return Json(model, new JsonSerializerSettings() { Formatting = Formatting.Indented });
        }
        [HttpPost("")]
        [ProducesResponseType(typeof(Match[]), 200)]
        [ProducesResponseType(404)]
        public IActionResult AddMatch([FromBody] MatchInputJson json)
        {
            var m = new Match(){Timestamp = DateTime.UtcNow};
            m.CopyPropertiesFrom(json);
            DB.Matches.Add(m);
            DB.SaveChanges();
            return GetMatch(m.Id);
        }
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            var c = DB.Matches.Find(id);
            if (c == null) return NotFound();
            return Json(c, new JsonSerializerSettings() { Formatting = Formatting.Indented });
        }
    }
}
