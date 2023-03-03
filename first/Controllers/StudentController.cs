using first.Data;
using first.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace first.Controllers
{
    public class StudentController : Controller
    {
        private readonly StudentDbContext _context;
        private  IWebHostEnvironment _env;
        public StudentController(StudentDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        // GET: StudentController
        public IActionResult Index()
        {
            IEnumerable<Student> stdlist = _context.Students;
            return View(stdlist);

        }

        // GET: StudentController/Details/5
        //public IActionResult Details(int id)
        //{
        //    return View();
        //}

        // GET: StudentController/Create
        public IActionResult Create()
        {
            var getFacultyName = _context.faculties.ToList();
            SelectList Facultylist = new SelectList(getFacultyName, "faculty_id", "faculty_name");
            ViewBag.FacultyListName = Facultylist;
            return View();
        }

        // POST: StudentController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Student stdlist)
        {
            
            if (ModelState.IsValid)
            {
                string uniqueFileName = GetUploadedFileName(stdlist);
                stdlist.imgUrl = uniqueFileName;

                _context.Students.Add(stdlist);
                await _context.SaveChangesAsync();
                TempData["ResultOk"] = "Recored is sucessfully added";
                return RedirectToAction("Index");
            }
            return View(stdlist);
        }
        private string GetUploadedFileName(Student stdlist)
        {
            string uniqueFileName = null;
            if (stdlist.Photo != null)
            {
                string uploadsFolder = Path.Combine(_env.WebRootPath, "img");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + stdlist.Photo.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using var fileStream = new FileStream(filePath, FileMode.Create);
                {
                    stdlist.Photo.CopyTo(fileStream);
                }

            }
            return uniqueFileName;
        }

        //GET: StudentController/Edit/5
        public IActionResult Edit(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return NotFound();
            }
            var stdfromdb = _context.Students.Find(Id);
            if (stdfromdb == null)
            {
                return NotFound();
            }
            return View(stdfromdb);
        }

        // POST: StudentController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Student student)
        {
            try
            {
                if (student == null)
                {
                    return NotFound();
                }
                if (ModelState.IsValid && student != null)
                {
                    _context.Students.Update(student);
                    _context.SaveChanges();
                    TempData["ResultOk"] = "Data Update sucessfully!";
                    return RedirectToAction("Index");
                }

            }
            catch (Exception ex)
            {
                NotFound(ex);
            }
            return View(student);
        }

        // GET: StudentController/Delete/5
        public IActionResult Delete(int Id)
        {
            if (Id == null || Id == 0)
            {
                return NotFound();
            }
            var empfromdb = _context.Students.Find(Id);

            if (empfromdb == null)
            {
                return NotFound();
            }
            return View(empfromdb);
        }

        // POST: StudentController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteEmp(int? Id, Student stdlist)
        {
            var deleterecord = _context.Students.Find(Id);
            if (deleterecord == null)
            {
                return NotFound();
            }
            _context.Students.Remove(deleterecord);
            _context.SaveChanges();
            TempData["ResultOk"] = "Data Deleted Successfully !";
            return RedirectToAction("Index");
        }
    }
}
