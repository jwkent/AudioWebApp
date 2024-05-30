using Microsoft.JSInterop;
using System.Reflection;

using System;
using System.Web;
using System.Text.RegularExpressions;
using Microsoft.Extensions.FileSystemGlobbing.Internal;

namespace AudioWebApp.Client.Services
{
    public class LinkShareService
    {
        private readonly IJSRuntime _jsruntime;
        
        public LinkShareService(IJSRuntime jSRuntime)
        {
            _jsruntime = jSRuntime;
        }
        public async Task CopyAudioLinkToClipboard(string source,string title)
        { 
            var encodedUrl = Uri.EscapeDataString(source);
            var link = $"https://localhost:8001/sharedplayer?url={encodedUrl}&title={title}";
        
            await _jsruntime.InvokeVoidAsync("shareAudioLink", link);   
        }
        public async Task SendAudioLinkSMS(string source, string title)
        {
            var encodedUrl = Uri.EscapeDataString(source);
            var link = $"https://localhost:8001/sharedplayer?url={encodedUrl}&title={title}";
            await _jsruntime.InvokeVoidAsync("sendSMS", link);
        }
       
    }
   
}
