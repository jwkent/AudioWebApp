using AudioWebApp.Client.Models;

namespace AudioWebApp.Client.Models
{
    public class Favorite
    {
        public string Name { get; set; }
        public string Source { get; set; }
        public DateTime DateTimeStamp { get; set; }
        public List<Message>? FavoriteQueuedList { get; set; }

        public Favorite(string name, string source, DateTime timeStamp, List<Message> queue)
        {
            Name = name;
            Source = source;
            DateTimeStamp = timeStamp;
            FavoriteQueuedList = queue;
        }
    }
}
