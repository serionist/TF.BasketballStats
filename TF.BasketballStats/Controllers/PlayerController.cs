using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using TF.BasketballStats.Database;
using TF.BasketballStats.Models.Players;

namespace TF.BasketballStats.Controllers
{
    [Route("api/players")]
    public class PlayerController:ApiController<PlayerController>
    {
        public PlayerController(ILogger<PlayerController> log, DatabaseContext db) : base(log, db)
        {
        }
        [HttpGet("")]
        [ProducesResponseType(typeof(Player[]),200)]
        public IActionResult GetPlayers()
        {
            return Json(DB.Players.OrderByDescending(e=>e.Timestamp).ToArray(), new JsonSerializerSettings() {Formatting = Formatting.Indented});
        }
        [HttpGet("{id:int}")]
        [ProducesResponseType(404)]
        [ProducesResponseType(typeof(Player), 200)]
        public IActionResult GetPlayer(int id)
        {
            var player = DB.Players.Find(id);
            if (player == null)
                return NotFound();
            return Json(player, new JsonSerializerSettings() {Formatting = Formatting.Indented});
        }
        [HttpPost("")]
        public IActionResult AddPlayer([FromBody]PlayerInputJson json)
        {
            var newModel = new Player()
            {
                Timestamp = DateTime.UtcNow
            };
            newModel.CopyPropertiesFrom(json);
            DB.Players.Add(newModel);
            DB.SaveChanges();
            return GetPlayer(newModel.Id);
        }
        [ProducesResponseType(404)]
        [ProducesResponseType(typeof(Player), 200)]
        [HttpPost("{id:int}")]
        public IActionResult UpdatePlayer(int id, [FromBody]PlayerInputJson json)
        {
            var player = DB.Players.Find(id);
            if (player == null)
                return NotFound();
            player.CopyPropertiesFrom(json);
            DB.SaveChanges();
            return GetPlayer(id);
        }
        [ProducesResponseType(404)]
        [ProducesResponseType(typeof(Player), 200)]
        [HttpPost("{id:int}/image")]
        public IActionResult SetProfile(int id)
        {
            IFormFile file = null;
            if (Request.Form?.Files == null || (file = Request.Form.Files.FirstOrDefault()) == null || !file.ContentType.StartsWith("image"))
                return BadRequest("No image provided, not an image, or more than 1 image is provided");
            

            var player = DB.Players.Find(id);
            if (player == null)
                return NotFound();
            
            using (var reader = new BinaryReader(file.OpenReadStream()))
                player.ProfilePicture = reader.ReadBytes((int)HttpContext.Request.ContentLength.Value);
            player.ProfilePictureMime = HttpContext.Request.ContentType;
            DB.SaveChanges();
            return GetPlayer(id);
        }
        [ProducesResponseType(404)]
        [ProducesResponseType(200)]
        [HttpGet("{id:int}/image")]
        public IActionResult GetProfile(int id)
        {
            
            var player = DB.Players.Find(id);
            if (player == null)
                return NotFound();
            if (player.ProfilePicture == null || player.ProfilePicture.Length == 0)
                return Redirect("/default-profile.png");
            return File(player.ProfilePicture, player.ProfilePictureMime);
        }
        [ProducesResponseType(404)]
        [ProducesResponseType(200)]
        [HttpDelete("{id:int}")]
        public IActionResult DeletePlayer(int id)
        {
            var player = DB.Players.Find(id);
            if (player == null)
                return NotFound();
            DB.Players.Remove(player);
            DB.SaveChanges();
            return Ok();
        }

    }
}
