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
            Subtopical subTopical1 = new Subtopical("sub1", "location of audio file");
            Subtopical subTopical2 = new Subtopical("sub2", "location of audio file");
            Subtopical subTopical3 = new Subtopical("sub3", "location of audio file");

            Subtopical[] subTopicals = new Subtopical[] { subTopical1, subTopical2, subTopical3 };
            
            Topical topical1 = new Topical("Topical1", subTopicals);
            Topical topical2 = new Topical("Topical2", subTopicals);
            Topical topical3 = new Topical("Topical3", subTopicals);

            IEnumerable<Topical> topicalData = new List<Topical>
             {
                 topical1, topical2, topical3
             };

            return topicalData;

        }
        
    }
}
