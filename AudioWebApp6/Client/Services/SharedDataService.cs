using AudioWebApp.Client.Models;
using MudBlazor;
using System.ComponentModel.Design;

namespace AudioWebApp.Client.Services
{
    public class SharedDataService
    {
        public string? AudioLink { get; set; }
        public string? AudioTitle { get; set; }
        public event Action OnPlayerToggle;
        public event Action OnShowContent;

        public void TogglePlayer()
        {
            OnPlayerToggle?.Invoke();
        }
        public void ShowMenu()
        {
            OnShowContent?.Invoke();
        } 
        public string GetServerPath(List<Server> servers, string serverName)
        {
            string serverPath = string.Empty;

            var serverLocation = servers.FirstOrDefault(id => id.Name == serverName);
            if (serverLocation != null)
            {
                serverPath = ReplaceHttpProtocolWithSecureProtocol(serverLocation.Location);
            }

            return serverPath;
        }

        private string ReplaceHttpProtocolWithSecureProtocol(string url)
        {
            var secureUrl = url;

            if (url.StartsWith("http://"))
            {
                secureUrl = "https" + url.Substring(4);
            }

            return secureUrl;
        }
        public List<Message> QueuedMessages { get; set; }
        public void GenerateQueue(List<Message> queue, string message)
        {
            QueuedMessages = queue.SkipWhile(item => item.Name != message).Skip(1).ToList();
            foreach(var i in QueuedMessages) { Console.WriteLine(i.Name); }
        }
    }
}
