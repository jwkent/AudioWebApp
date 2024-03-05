using AudioWebApp.Server.Services;
using AudioWebApp.Shared.Models;
using Newtonsoft.Json;

namespace AudioWebApp.Server.Utilities
{
    public class AnnoucementsFileWriter
    {
        private readonly HttpClient _httpClient;

        const string fileName = "./Files/AnnouncementData.json";
        public AnnoucementsFileWriter()
        {
                _httpClient = new HttpClient();
        }
        /// <summary>
        /// Creates a file with scrapped Announcements from TNP
        /// Only needed when an updated file is necessary or no file exists
        /// </summary>
        public async void WriteAnnouncements()
        {
            AnnouncementScrapperService service = new AnnouncementScrapperService(_httpClient);

            Announcement[] announcements = await service.ScrapeAnnouncements();

            AnnouncementData announcementData = new AnnouncementData(DateTime.Now, announcements);

            string json = JsonConvert.SerializeObject(announcementData);

            File.WriteAllText(fileName,json);

            AnnouncementsManager announcementsManager = new AnnouncementsManager();

        }
    }
}
