using AudioWebApp.Client.Models;
using Microsoft.JSInterop;
using Newtonsoft.Json;
using System.Collections.ObjectModel;


namespace AudioWebApp.Client.Services
{
    public class FavoritesService
    {
        private readonly IJSRuntime _jSRuntime;
       
        public IEnumerable<Favorite>? Favorites;
        public IEnumerable<Message>? Queue;
        public FavoritesService(IJSRuntime jSRuntime)
        {
            _jSRuntime = jSRuntime;
        }
        public async Task GetAllFavorites()
        {
            var result = await _jSRuntime.InvokeAsync<string>("getAllFavorites");
            Favorites = JsonConvert.DeserializeObject<ObservableCollection<Favorite>>(result)
                .OrderByDescending(item => item.DateTimeStamp);
        }
        public async Task RemoveFavorite(string key)
        {
            await _jSRuntime.InvokeVoidAsync("removeFavorite", key);
            await GetAllFavorites();
        }
        public async Task AddToFavorites(string key, string value, SharedDataService sharedDataService)
        {
            List<Message> queuedFavorites = new List<Message>();
            foreach(var item in sharedDataService.QueuedMessages)
            {
                Message message = new Message()
                {
                    Name = item.Name,
                    Link = Path.Combine(sharedDataService.QueuedMessagesServerPath, item.Link)
                };
                queuedFavorites.Add(message);
            }
             
            Favorite fav = new Favorite(key,value,DateTime.Now, queuedFavorites);
            await _jSRuntime.InvokeVoidAsync("setToFavorites", key, fav);
        }
        public async Task<IEnumerable<Message>> GetFavoriteQueue(string key)
        {
            var result = await _jSRuntime.InvokeAsync<string>("getFavoriteQueue",key);

            Queue = JsonConvert.DeserializeObject<ObservableCollection<Message>>(result);
           
            return Queue;
           
            
        }
    }
}
