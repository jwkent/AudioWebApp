namespace AudioWebApp.Server.Models
{
    public partial class Messages
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Link { get; set; }
        public int SeriesId { get; set; }
        public int Sequence { get; set; }
        public string Transcript { get; set; }

        public virtual Series Series { get; set; }
    }
}
