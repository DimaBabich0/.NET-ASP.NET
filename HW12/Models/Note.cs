namespace HW12.Models
{
    public class Note
    {
        public string Name { get; set; }
        public string Text { get; set; }
        public DateOnly Date { get; set; }
        public List<string> Tags { get; set; }
    }
}
