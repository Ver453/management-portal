
using first.Data;
using first.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace first.Controllers
{
    public class FileUploadController : Controller
    {
        private readonly StudentDbContext _context;
        private Microsoft.AspNetCore.Hosting.IHostingEnvironment _environment;
        public FileUploadController(StudentDbContext context, Microsoft.AspNetCore.Hosting.IHostingEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }
        public IActionResult Index()
        {
            IEnumerable<MultipleFileUpload> File = _context.Files;
            return View(File);
        }


        public IActionResult MultipleFileUpload()
        {
            MultipleFileUpload model = new MultipleFileUpload();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MultipleFileUpload(MultipleFileUpload model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = GetUploadedFileName(model);
                model.FilePath = uniqueFileName;

                _context.Files.Add(model);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(model);
        }
        private string GetUploadedFileName(MultipleFileUpload model)
        {
            string filename = null;
            if (model.Files.Count > 0)
            {
                foreach (IFormFile file in model.Files)
                {
                    string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/MultipleFileSave");
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    filename = Guid.NewGuid().ToString() + "_" + file.FileName;
                    string fileNamePath = Path.Combine(path, filename);
                    using var stream = new FileStream(fileNamePath, FileMode.Create);
                    {
                        file.CopyTo(stream);
                    }
                }
            }
            return filename;
        }

        public FileResult DownloadFile(string fileName)
        {
            //Build the File Path.
            string path = Path.Combine(this._environment.WebRootPath, "MultipleFileSave/") + fileName;

            //Read the File data into Byte Array.
            byte[] bytes = System.IO.File.ReadAllBytes(path);

            //Send the File to Download.
            return File(bytes, "application/octet-stream", fileName);
        }

        public IActionResult DeleteFile(int Id)
        {
            if (Id == null || Id == 0)
            {
                return NotFound();
            }
            var deletefile = _context.Files.Find(Id);

            if (deletefile == null)
            {
                return NotFound();
            }
            string delimg = deletefile.FilePath;
            delimg = Path.Combine(this._environment.WebRootPath, "MultipleFileSave", delimg);
            FileInfo fi = new FileInfo(delimg);
            if (fi != null)
            {
                System.IO.File.Delete(delimg);
                fi.Delete();
            }
            _context.Entry(deletefile).State = EntityState.Deleted;
            if (_context.SaveChanges() > 0)
            {
                if (System.IO.File.Exists(delimg))
                {
                    System.IO.File.Delete(delimg);
                }
                return RedirectToAction("Index");
            }
            
            return View();
            //From the wwwroot file delete....
            
        }
        //Next method without using IHosting environment....
        //public async Task<IActionResult> DownloadFile(string filePath)
        //{
        //            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/MultipleFileSave/") + filePath;
        //            var memory = new MemoryStream();

        //            using (var stream = new FileStream(path, FileMode.Open))
        //            {
        //                await stream.CopyToAsync(memory);
        //            }
        //            memory.Position = 0;
        //            var contentType = "application/octet-stream";
        //            var fileName = Path.GetFileName(path);
        //            return File(memory, contentType, fileName);
        //}
    }
}
