using AudioWebApp.Shared.Models;

namespace AudioWebApp.Client.Models
{
    public class ResponseContent
    {
        public object contentType { get; set; }
        public object jsonSerializerOptions { get; set; }
        public object statusCode { get; set; }
        public AnnouncementData value { get; set; }
    }
}
