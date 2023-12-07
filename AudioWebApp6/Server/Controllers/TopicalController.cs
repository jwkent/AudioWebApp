using Microsoft.AspNetCore.Mvc;
using AudioWebApp.Shared;

namespace AudioWebApp.Server.Controllers
{
    [ApiController]
    //[Route("[controller]")]
    [Route("api")]
    public class TopicalController : ControllerBase
    {
        [HttpGet("GetTopicals")]
        public IEnumerable<Topical> Get()
        {
            string[] subTopicals = { "sub1", "sub2", "sub3" };
            Topical topical1 = new Topical("Topical1", subTopicals);
            Topical topical2 = new Topical("Topical2", subTopicals);
            Topical topical3 = new Topical("Topical3", subTopicals);

            IEnumerable<Topical> x = new List<Topical>
             {
                 topical1, topical2, topical3
             };

            return x;

        }
        
    }
}
