using HtmlAgilityPack;
using Microsoft.AspNetCore.Mvc;
using AudioWebApp.Server.Utilities;

namespace AudioWebApp.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AnnouncementsController : Controller
    {
        [HttpGet("getannouncements")] 
        public IResult GetAnnouncements()
        {
            AnnouncementsManager manager = new AnnouncementsManager();

            return manager.GetAnnouncementsFile();
        }
    }
}
