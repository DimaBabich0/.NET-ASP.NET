using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HW09.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Security.Cryptography;
using System.Text;

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
            if (_context.Users.ToList().Count == 0)
            {
                ViewBag.ErrorMessage = "Wrong login or password";
                return View();
            }

            var users = _context.Users.Where(a => a.Login == login);
            if (users.ToList().Count == 0)
            {
                ViewBag.ErrorMessage = "Wrong login or password";
                return View();
            }

            var user = users.First();
            string? salt = user.Salt;
            if (string.IsNullOrEmpty(salt))
            {
                ViewBag.ErrorMessage = "Wrong login or password";
                return View();
            }

            string hash = MyCryptography.GenerateHash(salt, password);
            if (user.Password != hash.ToString())
            {
                ViewBag.ErrorMessage = "Wrong login or password";
                return View();
            }

            HttpContext.Session.SetString("UserLogin", user.Login);
            HttpContext.Session.SetInt32("UserId", user.Id);
            return RedirectToAction("Index");
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

            string salt = MyCryptography.GetSalt();
            string hash = MyCryptography.GenerateHash(salt, password);

            var newUser = new User
            {
                Login = login,
                Password = hash,
                Salt = salt
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
