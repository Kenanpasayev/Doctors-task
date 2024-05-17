using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApplication12.Models;
using WebApplication12.ViewModel;

namespace WebApplication12.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;

        public AccountController(UserManager<User>userManager)
        {
            _userManager = userManager;
        }
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterVm registerVm)
        {
            if (!ModelState.IsValid)return View();

            User appUser = new User()
            {
                UserName = registerVm.Name,

            };

            return Json(registerVm);
    
        }
    }
}
