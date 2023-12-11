using Newtonsoft.Json;

namespace AudioWebApp.Server.Models
{
    public class Item
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("path")]
        public string Path { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("sequence")]
        public int Sequence { get; set; }

        [JsonProperty("items")]
        //public object Items { get; set; }
        public List<Item> Items { get; set; }
    }
}
