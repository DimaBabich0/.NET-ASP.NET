using System.Text;
using HW12.Models;

namespace HW12.Services
{
    public interface ISave
    {
        void Save(List<Note> notes);
    }

    public class FileSave : ISave
    {
        private readonly string _filePath = "Notes.txt";

        public void Save(List<Note> notes)
        {
            StringBuilder sb = new StringBuilder();

            foreach (var note in notes)
            {
                sb.AppendLine($"Name: {note.Name}");
                sb.AppendLine($"Text: {note.Text}");
                sb.AppendLine($"Date: {note.Date}");
                sb.AppendLine($"Tags: {(note.Tags.Any() ? string.Join(", ", note.Tags) : "No tags")}");
                sb.AppendLine("");
            }
            File.WriteAllText(_filePath, sb.ToString());
        }
    }
}
