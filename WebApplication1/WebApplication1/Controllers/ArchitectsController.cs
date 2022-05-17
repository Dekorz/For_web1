using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ArchitectsController : Controller
    {
        [HttpGet(Name = "GetAchitect")]
        public IEnumerable<Architect> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new Architect
            {
                Name = "somename",
                Email = "someemail"
            })
            .ToArray();
        }
    }
}
