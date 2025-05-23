using Maturita_C_.Data;
using Maturita_C_.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;

namespace Maturita_C_.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDbContext _context;

        public AccountController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(string username, string password, bool consent)
        {
            if (!consent)
            {
                ModelState.AddModelError("Consent", "Musíte souhlasit s využitím dat AI.");
                return View();
            }

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                ModelState.AddModelError(string.Empty, "Vyplňte všechna pole.");
                return View();
            }

            if (_context.Users.Any(u => u.Username == username))
            {
                ModelState.AddModelError("Username", "Uživatel již existuje.");
                return View();
            }

            var user = new User
            {
                Username = username,
                PasswordHash = HashPassword(password),
                ConsentGiven = consent
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            return RedirectToAction("Login");
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            var hash = HashPassword(password);
            var user = _context.Users.FirstOrDefault(u => u.Username == username && u.PasswordHash == hash);

            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Špatné přihlašovací údaje.");
                return View();
            }

            HttpContext.Session.SetString("Username", user.Username);
            return RedirectToAction("Home");
        }

        public IActionResult Home()
        {
            var username = HttpContext.Session.GetString("Username");
            if (username == null) return RedirectToAction("Login");

            ViewBag.Username = username;
            return View();
        }

        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(password);
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }
    }
}
