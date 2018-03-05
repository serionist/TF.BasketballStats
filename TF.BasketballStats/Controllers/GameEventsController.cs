using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TF.BasketballStats.Database;

namespace TF.BasketballStats.Controllers
{
    [Route("api/matches")]
    public class GameEventsController:ApiController<GameEventsController>
    {
        public GameEventsController(ILogger<GameEventsController> log, DatabaseContext db) : base(log, db)
        {
        }
        [HttpGet("{matchId:int}/events")]
        [ProducesResponseType(typeof(GameEvent[]),200)]
        public IActionResult GetEvents(int matchId)
        {
            return Json(DB.GameEvents.Where(e => e.MatchId == matchId).ToArray());
        }

        [HttpPut("{matchId:int}/events")]
        [ProducesResponseType(typeof(GameEvent[]), 200)]
        public IActionResult AddEvents(int matchId, [FromBody] GameEvent[] events)
        {
            DB.GameEvents.AddRange(events.Select((e =>
            {
                e.MatchId = matchId;
                e.Timestamp = DateTime.UtcNow;
                return e;
            })));
            DB.SaveChanges();
            return Json(events);
        }

        [HttpDelete("{matchId:int}/events")]
        [ProducesResponseType(200)]
        public IActionResult DeleteEvents(int matchId, [FromBody] int[] eventIds)
        {
            DB.GameEvents.RemoveRange(DB.GameEvents.Where(e=>eventIds.Contains(e.Id) && e.MatchId == matchId));
            DB.SaveChanges();
            return Ok();
        }

        [HttpPost("{matchId:int}/events/{id:int}")]
        [ProducesResponseType(typeof(GameEvent), 200)]
        [ProducesResponseType(404)]
        public IActionResult UpdateEvent(int matchId, int id, [FromBody]GameEvent ev)
        {
            var model = DB.GameEvents.FirstOrDefault(e => e.Id == id && e.MatchId == matchId);
            if (model == null)
                return NotFound();
            model.CopyPropertiesFrom(ev, nameof(model.Id));
            DB.SaveChanges();
            return Json(model);


        }

    }
}
