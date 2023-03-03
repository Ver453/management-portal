using first.Data;
using first.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace first.Controllers
{
    public class FacultyController : Controller
    {
        private readonly StudentDbContext _context;
        public FacultyController(StudentDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        // GET: StudentController/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: StudentController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Faculty faculty)
        {

            if (ModelState.IsValid)
            { 
                _context.faculties.Add(faculty);
                await _context.SaveChangesAsync();
                TempData["ResultOk"] = "Recored is sucessfully added";
                return RedirectToAction("Create");
            }
            return View(faculty);
        }
    }
}
