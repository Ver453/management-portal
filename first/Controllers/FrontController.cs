using first.Data;
using first.Migrations;
using first.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace first.Controllers
{
    public class FrontController : Controller
    {

        private readonly StudentDbContext _context;
        public FrontController(StudentDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<MenuModel> menu = _context.menus.ToList();
            return View(menu);
        }

        public IActionResult Team()
        {
            return View();

        }
        public IActionResult Courses()
        {
            List<MenuModel> menu = _context.menus.ToList();
            return View(menu);
        }

        //private IActionResult View(List<MenuModel> menu, IEnumerable<Student> stdlist)
        //{
        //    throw new NotImplementedException();
        //}

        public IActionResult About()
        {
            return View();
        }
        public IActionResult Contact()
        {
            return View();
        }
    }
}
