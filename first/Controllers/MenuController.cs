using first.Data;
using first.Models;
using Microsoft.AspNetCore.Mvc;

namespace first.Controllers
{
    public class MenuController : Controller
    {
        private readonly StudentDbContext _context;
        public MenuController(StudentDbContext context)
        {
            _context = context;
        }
        //Create a new instance of the MenuModel class
       MenuModel menu = new MenuModel();
        //Define a method to generate the menu as a partial view
        public PartialViewResult GenerateMenu()
        {
            var menuList = _context.menus.ToList();
            var menuForDisplay = menu.MenuTree(menuList, null);
            return PartialView("_GenerateMenu", menuForDisplay);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MenuModel menulst)
        {
            if (ModelState.IsValid)
            {
                _context.menus.Add(menulst);
                await _context.SaveChangesAsync();
                TempData["ResultOk"] = "Recored is sucessfully added";
                return RedirectToAction("Create");
            }
            return View(menulst);
        }
    }
}
