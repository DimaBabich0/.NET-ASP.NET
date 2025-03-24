using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HW09.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HW09.Controllers
{
    public class GuestBookController : Controller
    {
        private readonly GuestBookContext _context;

        public GuestBookController(GuestBookContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var messages = await _context.Messages
                .Include(m => m.User)
                .OrderByDescending(m => m.MessageDate)
                .ToListAsync();

            ViewBag.UserLogin = HttpContext.Session.GetString("UserLogin");

            return View(messages);
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string login, string password)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Login == login && u.Password == password);

            if (user != null)
            {
                HttpContext.Session.SetString("UserLogin", user.Login);
                HttpContext.Session.SetInt32("UserId", user.Id);
                return RedirectToAction("Index");
            }

            ViewBag.ErrorMessage = "Wrong login or password";
            return View();
        }

        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Registration(string login, string password)
        {
            if (await _context.Users.AnyAsync(u => u.Login == login))
            {
                ViewBag.ErrorMessage = "User with such login already exists";
                return View();
            }

            var newUser = new User
            {
                Login = login,
                Password = password
            };

            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            HttpContext.Session.SetInt32("UserId", newUser.Id);
            HttpContext.Session.SetString("UserLogin", newUser.Login);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> AddMessage(string text)
        {
            var userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
                return RedirectToAction("Login");

            if (string.IsNullOrWhiteSpace(text))
            {
                ViewBag.ErrorMessage = "Message can't be empty";
                return RedirectToAction("Index");
            }

            var item = new Message
            {
                Id_User = userId.Value,
                Text = text,
                MessageDate = DateTime.Now
            };

            _context.Messages.Add(item);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
    }
}
