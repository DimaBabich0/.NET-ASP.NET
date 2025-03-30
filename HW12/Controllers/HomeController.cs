using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using HW12.Models;
using HW12.Services;

namespace HW12.Controllers
{
    public class HomeController : Controller
    {
        private readonly IDisplay _webDisplay;
        private readonly ISave _fileDisplay;
        private readonly List<Note> _notes;

        public HomeController(IDisplay webDisplay, ISave fileDisplay)
        {
            _webDisplay = webDisplay;
            _fileDisplay = fileDisplay;

            _notes = new List<Note>
            {
                new Note
                { 
                    Name = "Note #1",
                    Text = "Text",
                    Date = DateOnly.FromDateTime(DateTime.Now),
                    Tags = new List<string>
                    {
                        "Tag #1",
                        "Tag #2"
                    }
                }
            };
        }

        public IActionResult Index(string message)
        {
            ViewBag.Message = message;
            return View();
        }

        public IActionResult DisplayNotes()
        {
            _webDisplay.Display(_notes);
            return View("Index", _notes);
        }

        public IActionResult SaveToFile()
        {
            _fileDisplay.Save(_notes);
            return RedirectToAction("Index", new { message = "Notes saved to file" });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
