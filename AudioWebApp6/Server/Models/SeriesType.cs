namespace AudioWebApp.Server.Models
{
    public partial class SeriesType
    {
        public SeriesType()
        {
            Series = new HashSet<Series>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Series> Series { get; set; }
    }
}
