using AudioWebApp.Client.Models;
using MudBlazor;

namespace AudioWebApp.Client.Services
{
    public class SharedDataService
    {
        public string? AudioLink { get; set; }
        public string? AudioTitle { get; set; }
        public event Action OnPlayerToggle;

        public void TogglePlayer()
        {
            OnPlayerToggle?.Invoke();
        }

        public string GetServerPath(List<Server> servers, string serverName)
        {
            string serverPath = string.Empty;

            var serverLocation = servers.FirstOrDefault(id => id.Name == serverName);
            if (serverLocation != null)
            {
                serverPath = serverLocation.Location;
            }

            return serverPath;
        }
    }
}
