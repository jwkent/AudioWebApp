using AudioWebApp.Shared.Models;
using HtmlAgilityPack;
using System.Text.RegularExpressions;

namespace AudioWebApp.Server.Services
{
    public class AnnouncementScrapperService
    {
        private readonly HttpClient _httpClient;
        string url = "https://wwww.thenarrowpath.com/announcements.php";

        public AnnouncementScrapperService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<Announcement[]> ScrapeAnnouncements()
        {
            Announcement[] titles = await ScrapeAnnouncementTitles();
            var body = await ScrapeAnnouncementData();
            var announcements = JoinAnnouncementData(titles, body);
            return announcements;
        }
        public async Task<Announcement[]> ScrapeAnnouncementTitles()
        {
            var html = await _httpClient.GetStringAsync(url);
            var document = new HtmlDocument();
            document.LoadHtml(html);
            List<string> spans = new List<string>();
            HtmlNodeCollection spanNodes = document.DocumentNode.SelectNodes("//td[1]//span");

            if(spanNodes != null )
            {
                foreach(HtmlNode spanNode in spanNodes )
                {
                    spans.Add(spanNode.InnerText);
                }
            }
            var spanText = FilterTitles(spans); 
            return spanText.ToArray();
        }
        public List<Announcement> FilterTitles(List<string> inputStrings)
        {
            List<Announcement> Announcements = new List<Announcement>();
            for( int i = 0; i < inputStrings.Count; i++ )
            {
                Announcements.Add(new Announcement { Title = inputStrings[i], IsNew = false });
                if (inputStrings[i] == " NEW!")
                {
                    int indexOfItemBeforeNew = Announcements.Count - 2;
                    Announcements[indexOfItemBeforeNew].IsNew = true;
                }
            }
            Announcements.RemoveAll(Announcement => Announcement.Title == " NEW!");
            return Announcements;
        }
        public async Task<List<string>> ScrapeAnnouncementData()
        {
            var html = await _httpClient.GetStringAsync(url);
            var document = new HtmlDocument();
            document.LoadHtml(html);
            List<string> body = new List<string>();
            HtmlNodeCollection spanElement = document.DocumentNode.SelectNodes("//td[1]");
            if(spanElement != null )
            {
                foreach(HtmlNode spanNode in spanElement )
                {
                    string result = FilterBodyText(string.Join("", spanNode.InnerHtml));
                    body.Add(result);
                }
            }
            return body;
        }
        static string FilterBodyText(string inputString)
        {
            string pattern = @"<br>(.*)";
            Match match = Regex.Match(inputString, pattern);

            if (match.Success)
            {
                pattern = @"<br>|&bull;|;|\s+";
                string processed = Regex.Replace(match.Groups[1].Value, pattern, " ");
                string patternSpace = @"\s+";
                string processedSpaces = Regex.Replace(processed, patternSpace, " ");
                return processedSpaces;
            }
            else
            {
                return string.Empty;
            }

        }
        private Announcement[] JoinAnnouncementData(Announcement[] announcements, List<string> bodyText)
        {
            for(int i = 0; i < bodyText.Count; i++)
            {
                announcements[i].BodyText = bodyText[i];
            }
            return announcements;
        }
    }
}
