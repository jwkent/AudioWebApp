namespace AudioWebApp.Server.Models
{
    public partial class Category
    {
        public Category()
        {
            Series = new HashSet<Series>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int Count { get; set; }

        public virtual ICollection<Series> Series { get; set; }
    }
}
