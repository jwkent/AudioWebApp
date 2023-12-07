namespace AudioWebApp.Shared
{
    public class Topical
    {
        public string Name { get; set; }
        public Subtopical[] Subtopical { get; set; }

        public Topical(string name, Subtopical[] subtopical)
        {
            Name = name;
            Subtopical = subtopical;
        }
    }

}
