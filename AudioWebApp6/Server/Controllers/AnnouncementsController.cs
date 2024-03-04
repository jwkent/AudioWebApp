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
        /// <summary>
        /// This is a testing endpoint that writes an AnnouncementData.json
        /// file. It scrapes the announcements and will controlled by a Quartz.Net job.
        /// Use as needed while in production. 
        /// </summary>
        [HttpGet("writeannouncements")]
        public void WriteAnnouncements()
        {
            AnnoucementsFileWriter writer = new AnnoucementsFileWriter();
            writer.WriteAnnouncements();
        }
 
        [HttpGet("getupdate/{date}")]
        public bool GetUpdate(DateTime date)
        {
            AnnouncementsManager manager = new AnnouncementsManager();
            bool isUpdate =  manager.Update(date);
            return isUpdate;
        }
    }
}
