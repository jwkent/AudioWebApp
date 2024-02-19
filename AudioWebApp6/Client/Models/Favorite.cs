namespace AudioWebApp.Client.Models
{
    public class Favorite
    {
        public string Name { get; set; }
        public string Source { get; set; }
        public DateTime DateTimeStamp { get; set; }

        public Favorite(string name, string source, DateTime timeStamp)
        {
            Name = name;
            Source = source;
            DateTimeStamp = timeStamp;
        }
    }
}
