using AudioWebApp.Client.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace AudioWebApp.Client.Services
{
    public class CompletedService
    {
        private readonly IJSRuntime _jsruntime;
        
        public ObservableCollection<CompletedMessages>? Completed;
        public List<string>? completedMessages;
        public CompletedService(IJSRuntime JSRuntime) 
        {
            _jsruntime = JSRuntime;
        }
        public async Task GetCompletedMessages(string seriesName)
        {
            // Data : key is audioSeries ---> returned value
            // is an IEnumerable of CompletedMessages [ CategoryTitle, [MessageTitles] ] 
            var result = await _jsruntime.InvokeAsync<string>("getCompletedSeries",seriesName);
            Completed = JsonConvert.DeserializeObject<ObservableCollection<CompletedMessages>>(result);
            
        }
        
        public bool IsCompleted(string message)
        {
            if(Completed == null || !Completed.Any())
            {
                return false;
            }
            else
            {
                return Completed.Any(cm => cm.MessageTitles.Any(mt => mt.Equals(message, StringComparison.OrdinalIgnoreCase)));
            }
        }
    }
    public class CompletedMessages
    {
        public string? CategoryTitle { get; set; }
        public List<string>? MessageTitles { get; set; }
        
    }

}
