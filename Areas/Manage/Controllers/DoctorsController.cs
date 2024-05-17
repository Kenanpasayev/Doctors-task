using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication12.DAL;
using WebApplication12.Models;

namespace WebApplication12.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class DoctorsController : Controller
    {
        private readonly AppDbContext _context;

        public IWebHostEnvironment _environment { get; }

        public DoctorsController(AppDbContext context,IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }
        public IActionResult Index()
        {
            return View(_context.Doctors.ToList());
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Doctors doctors) 
        {
            if(!ModelState.IsValid)
            {
                return View(doctors);
            }
            string path = _environment.WebRootPath + @"\Upload\Doctor\";
            string filname = Guid.NewGuid() + doctors.ImgFile.FileName;
            using (FileStream stream = new FileStream(path + filname, FileMode.Create))
            {
                doctors.ImgFile.CopyTo(stream);
            }
            doctors.ImgUrl = filname;
            _context.Doctors.Add(doctors);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
        public IActionResult Update(int id)
        {
            Doctors doctors = _context.Doctors.FirstOrDefault(x => x.Id == id);
            if(doctors == null)
            {
                return RedirectToAction("Index");
            }
            return View(doctors);
        }
        [HttpPost]
        public IActionResult Update(Doctors doctors)
        {
            Doctors olddoctors = _context.Doctors.FirstOrDefault(x => x.Id == doctors.Id);
            if (olddoctors == null) return NotFound();
            if (doctors.ImgFile != null)
            {
                string path = _environment.WebRootPath + @"\Upload\Doctor\";
                string filname = Guid.NewGuid() + doctors.ImgFile.FileName;
                using (FileStream stream = new FileStream(path + filname, FileMode.Create))
                {
                    doctors.ImgFile.CopyTo(stream);
                }
                olddoctors.ImgUrl = filname;
            }

            doctors.Name = doctors.Name;
            doctors.Description = doctors.Description;
            _context.SaveChanges();

            return RedirectToAction("Index");

        }
        public IActionResult Delete(int id) 
        {
            var doctor = _context.Doctors.FirstOrDefault(x => x.Id == id);
            if (doctor != null)
            {
                string path = _environment.WebRootPath + @"\Upload\Doctor\"+doctor.ImgUrl;
                FileInfo fileInfo = new FileInfo(path);
                if (fileInfo.Exists)
                {
                    fileInfo.Delete();
                }
                _context.Doctors.Remove(doctor);
                _context.SaveChanges();
                return RedirectToAction("index");
            }
            return BadRequest();
        }
    }
}
