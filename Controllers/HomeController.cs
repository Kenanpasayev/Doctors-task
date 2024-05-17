using Microsoft.AspNetCore.Mvc;
using WebApplication12.DAL;

namespace WebApplication12.Controllers
{
    public class HomeController : Controller
    {

       public AppDbContext _dbContext;
        public HomeController( AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            var data= _dbContext.Doctors.ToList();
            return View(data);
        }

    }
}