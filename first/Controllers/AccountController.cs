using first.Data;
using first.Models;
using Microsoft.AspNetCore.Mvc;

namespace first.Controllers
{
    public class AccountController : Controller
    {
        private readonly StudentDbContext _context;
        public AccountController(StudentDbContext context) 
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
       
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task <IActionResult>Register(AccountModel account)
        {
            if (ModelState.IsValid)
            {
                _context.accounts.Add(account);
                await _context.SaveChangesAsync();
                return RedirectToAction("Register");
            }
            return View(account);
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(AccountModel account)
        {
            if (ModelState.IsValid)
            {
                var usr = _context.accounts.Single(u => u.Email == account.Email && u.Password == account.Password);
                if (usr != null)
                {
                    _context.accounts.Add(usr);
                    return RedirectToAction("Login");
                }
                else
                {
                    ModelState.AddModelError("", "username and password is wrong");
                }
            }
            return View(account);
        }
    }
}
