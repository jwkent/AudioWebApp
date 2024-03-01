using Microsoft.JSInterop;
using AudioWebApp.Shared.Models;
using Newtonsoft.Json;

namespace AudioWebApp.Client.Services
{

    public class AnnouncementService
    {
        private HttpClient _httpClient;
        private readonly IJSRuntime _jsRuntime;

        public AnnouncementService(IJSRuntime jSRuntime, HttpClient httpClient)
        {
            _httpClient = httpClient;
            _jsRuntime = jSRuntime;
        }
        public IEnumerable<Announcement> announcements = Array.Empty<Announcement>();
        public string announcementDataDateStamp;

        public async Task LoadAnnouncements()
        {
            // check to see if there is a file in local storage
            var response = await _httpClient.GetAsync("https://localhost:8001/api/announcements/getannouncements");
            if (response.IsSuccessStatusCode)
            {
                var annData = await response.Content.ReadAsStringAsync();
                annData = JsonConvert.DeserializeObject<string>(annData);
                await SetItemAsync("AnnouncementData", annData);
            }

            // if the data is in LocalForage
            string result = await GetItemsAsync("AnnouncementData");

            if (result != null)
            {
                try
                {
                    List<Announcement> announcementResults = new List<Announcement>();
                    AnnouncementData? announcementData = JsonConvert.DeserializeObject<AnnouncementData>(result);
                    foreach(var a in announcementData.Announcements)
                    {
                        announcementResults.Add(a);
                    }
                    announcements = announcementResults.ToArray();
                    announcementDataDateStamp = announcementData.DateStamp.ToString();
                }
                catch (Exception ex)
                {
                    if (ex.InnerException != null)
                    {
                        Console.WriteLine(ex.InnerException.Message.ToString());
                    }
                    else
                    {
                        Console.WriteLine(ex.Message.ToString());
                    }
                }
            }
        }
        private async Task SetItemAsync(string key, string value)
        {
            await _jsRuntime.InvokeVoidAsync("localforage.setItem", key, value);
        }
        private async Task<string> GetItemsAsync(string key)
        {
            return await _jsRuntime.InvokeAsync<string>("localforage.getItem", key);
        }
    }
}
