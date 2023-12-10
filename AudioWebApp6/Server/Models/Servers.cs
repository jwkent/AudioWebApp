namespace AudioWebApp.Server.Models
{
    public partial class Servers
    {
        public Servers()
        {
            Series = new HashSet<Series>();
        }

        public string Name { get; set; }
        public string Location { get; set; }
        public int Id { get; set; }

        public virtual ICollection<Series> Series { get; set; }
    }
}
