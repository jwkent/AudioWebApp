namespace AudioWebApp.Server.Models
{
    public partial class Series
    {
        public Series()
        {
            Messages = new HashSet<Messages>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int ServerId { get; set; }
        public int CategoryId { get; set; }
        public int SeriesTypeId { get; set; }
        public int Sequence { get; set; }

        public virtual ICollection<Messages> Messages { get; set; }
        public virtual Category Category { get; set; }
        public virtual SeriesType SeriesType { get; set; }
        public virtual Servers Server { get; set; }
    }
}
