using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HW09.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Security.Cryptography;
using System.Text;
using HW09.Repository;

namespace HW09.Controllers
{
    public class GuestBookController : Controller
    {
        IRepository repo;

        public GuestBookController(IRepository repo)
        {
            this.repo = repo;
        }

        public async Task<IActionResult> Index()
        {
            var messages = await repo.GetMessageList();

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
            List<User> users = await repo.GetUsersList(login);
            if (users.Count == 0)
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
            List<User> users = await repo.GetUsersList(login);
            if (users.Count != 0)
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

            await repo.Create(newUser);
            await repo.Save();

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

            await repo.Create(item);
            await repo.Save();


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
