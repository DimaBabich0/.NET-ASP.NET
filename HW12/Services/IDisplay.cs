using HW12.Models;

namespace HW12.Services
{
    public interface IDisplay
    {
        void Display(List<Note> notes);
    }

    public class WebDisplay : IDisplay
    {
        public void Display(List<Note> notes)
        {
            foreach (var note in notes)
            {
                Console.WriteLine($"Name: {note.Name}");
                Console.WriteLine($"Text: {note.Text}");
                Console.WriteLine($"Date: {note.Date}");
                Console.Write($"Tags: ");
                foreach (var tag in note.Tags)
                    Console.Write(tag + "; ");
                Console.Write("\n");
                Console.WriteLine("--------------");
            }
            Console.WriteLine();
        }
    }
}
