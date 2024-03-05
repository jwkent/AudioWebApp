using Microsoft.JSInterop;
using AudioWebApp.Shared.Models;
using AudioWebApp.Client.Models;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Text.Json;
using System.Globalization;
using System.Data;

namespace AudioWebApp.Client.Services
{

    public class AnnouncementService
    {
        private HttpClient _httpClient;
        private readonly IJSRuntime _jsRuntime;

        public AnnouncementService(IJSRuntime jSRuntime, HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.Timeout = TimeSpan.FromSeconds(5);
            _jsRuntime = jSRuntime;
        }
        const string server = "api/announcements/";
        private const string _getAnnouncementsEndpoint = $"{server}getannouncements";
        private const string _getAnnouncementsDate = $"{server}getlastupdateddate";
        private const string _getUpdate = $"{server}getupdate/";
        public IEnumerable<Announcement> announcements = Array.Empty<Announcement>();
        public DateTime announcementDataDateStamp;
        /// <summary>
        /// Called by Index.razor when initialized to get the announcementData
        /// Checks localforage first then calls api if not stored locally
        /// </summary>
        /// <returns></returns>
        public async Task LoadAnnouncements()
        {

            bool instorage = await GetAnnouncements();
            if (!instorage) 
            {
                await GetAnnouncementsFile();
            }
        }
        /// <summary>
        /// Looks in localforage for announcementData entry
        /// If there it call the process function.
        /// If not returns false
        /// </summary>
        /// <returns>bool</returns>
        public async Task<bool> GetAnnouncements()
        {
           string results = await GetItemsAsync("AnnouncementData");
            if (results != null) 
            {
                ProcessAnnouncements(results);
                
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// Issues a Get request to the server for announcementData
        /// Stores the result in localforage
        /// </summary>
        /// <returns></returns>
        public async Task GetAnnouncementsFile()
        {
            var response = await _httpClient.GetAsync(_getAnnouncementsEndpoint);
            if (response.IsSuccessStatusCode)
            {
                var annData = await response.Content.ReadAsStringAsync();

                await SetItemAsync("AnnouncementData", annData);
                await GetAnnouncements();
            }
        }
        /// <summary>
        /// Converts the announcementData and passes it up to the UI
        /// </summary>
        /// <param name="results"></param>
        public async void ProcessAnnouncements(string results)
        {
            try
            {
                List<Announcement> announcementResults = new List<Announcement>();

                ResponseContent content = JsonConvert.DeserializeObject<ResponseContent>(results);

                foreach (var a in content.value.Announcements)
                {
                    announcementResults.Add(a);
                }
                announcements = announcementResults.ToArray();
                announcementDataDateStamp = content.value.DateStamp;
                // THIS Should be handled better ???
                bool isUpdate = await CheckUpdate(announcementDataDateStamp);

                if(isUpdate) { await GetAnnouncementsFile(); }
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
        
        private async  Task<bool>  CheckUpdate(DateTime dateTime)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = _httpClient.BaseAddress;
            bool.TryParse(await httpClient.GetStringAsync($"{_getUpdate}{dateTime}"), out var boolResult);
            return boolResult;
        }
        /// <summary>
        /// Standard Issue localforage javascript setter
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private async Task SetItemAsync(string key, string value)
        {
            await _jsRuntime.InvokeVoidAsync("localforage.setItem", key, value);
        }
        /// <summary>
        /// Checks to see if the key is stored in localforage
        /// </summary>
        /// <param name="key"></param>
        /// <returns>Either a value if key exists or null if it does not</returns>
        private async Task<string> GetItemsAsync(string key)
        {
            return await _jsRuntime.InvokeAsync<string>("getAnnouncements", key);;
        }
        
    }
    
}
