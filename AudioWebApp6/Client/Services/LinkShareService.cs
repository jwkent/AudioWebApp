using Microsoft.JSInterop;
using System.Reflection;

using System;
using System.Web;
using System.Text.RegularExpressions;

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
            string editedSource = RemoveCharacters(source);

            string unicodeSource = HttpUtility.UrlEncode(editedSource);
            string unicodeTitle = HttpUtility.UrlEncode(title);
            Console.WriteLine("encoded: " + unicodeSource);
            string linkParams = $"{unicodeTitle}/{unicodeSource}";
            string link = "https://localhost:8001/sharedplayer/" + linkParams;
        
            await _jsruntime.InvokeVoidAsync("shareAudioLink", link); 
            Console.WriteLine("Link: " + link);
        }
        public string RemoveCharacters(string url)
        {
            string pattern = @"^.+\.com";
            string result = Regex.Replace(url, pattern, "");
            string pattern1 = @"\.mp3$";
            string result1 = Regex.Replace(result, pattern1, "");
            return result1;
        }
    }
}
