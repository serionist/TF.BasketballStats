using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using TF.BasketballStats.Database;

namespace TF.BasketballStats.Controllers
{
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    [Produces("application/json")]
    public abstract class ApiController<T>:Controller where T:Controller
    {
        protected ILogger Log { get; }
        protected  DatabaseContext DB { get; set; }
        protected ApiController(ILogger<T> log, DatabaseContext db)
        {
            DB = db;
            Log = log;
        }

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (context.ModelState?.IsValid == false)
            {
                 context.Result = BadRequest(ModelState);
                return;
            }
            await base.OnActionExecutionAsync(context, next);
        }
    }
}
