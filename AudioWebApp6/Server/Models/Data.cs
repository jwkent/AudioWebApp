using Newtonsoft.Json;

namespace AudioWebApp.Server.Models
{
    public class Data
    {
        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("items")]
        public List<Item> Results { get; set; }
    }
}
