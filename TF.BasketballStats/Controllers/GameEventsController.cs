using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TF.BasketballStats.Database;

namespace TF.BasketballStats.Controllers
{
    [Route("api/events")]
    public class GameEventsController:ApiController<GameEventsController>
    {
        public GameEventsController(ILogger<GameEventsController> log, DatabaseContext db) : base(log, db)
        {
        }


    }
}
