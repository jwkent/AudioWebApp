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
        public async Task AddToFavorites(string key, string category, string source)
        {
            Favorite fav = new Favorite(key,category,source,DateTime.Now);
            await _jSRuntime.InvokeVoidAsync("setToFavorites", key, fav);
        }
    }
}
