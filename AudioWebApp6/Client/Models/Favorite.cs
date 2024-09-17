namespace AudioWebApp.Client.Models
{
    public class Favorite
    {
        public string Name { get; set; }
        public string Series { get; set; }
        public string Source { get; set; }
        public DateTime DateTimeStamp { get; set; }

        public Favorite(string name, string series, string source, DateTime timeStamp)
        {
            Name = name;
            Series = series;
            Source = source;
            DateTimeStamp = timeStamp;
        }
    }
}
