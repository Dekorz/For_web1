
using EasyNetQ;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
namespace WebApplication1.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BuildingsController : ControllerBase
    {
        private readonly IWebApplication1Storage _db;
        private readonly IBus _bus;

        public BuildingsController(IWebApplication1Storage db)
        {
            this._db = db;
        }

        const int PAGE_SIZE = 25;

        [HttpGet]
        [Produces("application/hal+json")]
        public IActionResult Get(int index = 0, int count = PAGE_SIZE)
        {
            var items = _db.ListBuildings().Skip(index).Take(count)
                .Select(v => v.ToResource());
            var total = _db.CountBuildings();
            var _links = HAL.PaginateAsDynamic("/Buildings", index, count, total);
            var result = new
            {
                _links,
                count,
                total,
                index,
                items
            };
            return Ok(result);
        }

        //[HttpGet("{id}")]
        //[Produces("application/hal+json")]
        //public IActionResult Get(string id)
        //{
        //    var building = _db.FindBuilding(id);
        //    if (building == default) return NotFound();
        //    var resource = building.ToResource();
        //    resource._actions = new
        //    {
        //        delete = new
        //        {
        //            href = $"/api/buildings/{id}",
        //            method = "DELETE",
        //            name = $"Delete {id} from the database"
        //        }
        //    };
        //    return Ok(resource);
        //}
    }
}
