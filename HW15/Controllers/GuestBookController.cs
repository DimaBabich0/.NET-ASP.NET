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

            return PartialView(messages);
        }

        public IActionResult Login()
        {
            return PartialView("Login");
        }

        [HttpPost]
        public async Task<IActionResult> Login(string login, string password)
        {
            List<User> users = await repo.GetUsersList(login);
            if (users.Count == 0)
            {
                ViewBag.ErrorMessage = "Wrong login or password";
                return PartialView("Login");
            }

            var user = users.First();
            string? salt = user.Salt;
            if (string.IsNullOrEmpty(salt))
            {
                ViewBag.ErrorMessage = "Wrong login or password";
                return PartialView("Login");
            }

            string hash = MyCryptography.GenerateHash(salt, password);
            if (user.Password != hash.ToString())
            {
                ViewBag.ErrorMessage = "Wrong login or password";
                return PartialView("Login");
            }

            HttpContext.Session.SetString("UserLogin", user.Login);
            HttpContext.Session.SetInt32("UserId", user.Id);
            return Json(new { success = true });
        }

        public IActionResult Registration()
        {
            return PartialView("Registration");
        }

        [HttpPost]
        public async Task<IActionResult> Registration(string login, string password)
        {
            List<User> users = await repo.GetUsersList(login);
            if (users.Count != 0)
            {
                ViewBag.ErrorMessage = "User with such login already exists";
                return PartialView("Registration");
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
            return Json(new { success = true });
        }

        [HttpPost]
        public async Task<IActionResult> AddMessage(string text)
        {
            var userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
                return PartialView("Login");

            if (string.IsNullOrWhiteSpace(text))
            {
                ViewBag.ErrorMessage = "Message can't be empty";
                return PartialView("Index");
            }

            var item = new Message
            {
                Id_User = userId.Value,
                Text = text,
                MessageDate = DateTime.Now
            };

            await repo.Create(item);
            await repo.Save();

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction(nameof(Index));
        }
    }
}
