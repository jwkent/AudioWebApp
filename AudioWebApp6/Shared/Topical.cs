namespace AudioWebApp.Shared
{
    public class Topical
    {
        public string Name { get; set; }
        public string[] Subtopical { get; set; }

        public Topical(string name, string[] subtopical)
        {
            Name = name;
            Subtopical = subtopical;
        }
    }

}
