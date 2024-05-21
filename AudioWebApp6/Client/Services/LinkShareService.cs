using Microsoft.JSInterop;
using System.Reflection;

using System;
using System.Web;

namespace AudioWebApp.Client.Services
{
    public class LinkShareService
    {
        private readonly IJSRuntime _jsruntime;

        public LinkShareService(IJSRuntime jSRuntime)
        {
            _jsruntime = jSRuntime;
        }
        public async Task CopyAudioLinkToClipboard(string title, string source)
        {
            string unicodeSource = HttpUtility.UrlEncode(source);
            Console.WriteLine("encoded: " + unicodeSource);
            string linkParams = $"{title}/{unicodeSource}";
            string link = "https://localhost:8001/sharedplayer/" + linkParams;
            
            await _jsruntime.InvokeVoidAsync("shareAudioLink", link); 
            Console.WriteLine(link);
        }
    }
}
