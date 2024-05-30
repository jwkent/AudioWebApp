using Microsoft.JSInterop;
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

        public async Task CopyAudioLinkToClipboard(string source,string title)
        {
            string link = BuildLink(source, title);

            await _jsruntime.InvokeVoidAsync("shareAudioLink", link);
        }

        public async Task SendAudioLinkSMS(string source, string title)
        {
            string link = BuildLink(source, title);

            await _jsruntime.InvokeVoidAsync("sendSMS", link);
        }

        private string BuildLink(string source, string title)
        {
            string appName = "theNarrowPath.wvss.biz";  // "theNarrowPath.app"
            string protocol = "http";   // "https"

            var encodedUrl = Uri.EscapeDataString(source);
            var encodeTitle = HttpUtility.UrlEncode(title);

            var link = $"{protocol}://{appName}/sharedplayer?url={encodedUrl}&title={encodeTitle}";

            return link;
        }
    }
   
}
