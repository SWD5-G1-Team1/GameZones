using Game.DAL.Entities;
using GameZone2.PL.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Game.PL.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public AccountController(UserManager<ApplicationUser> userManager , SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [HttpGet] 
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> RegisterAsync(RegisterViewModel registerViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser()
                {
                    UserName = registerViewModel.Email.Split('@')[0],
                    FullName = registerViewModel.FullName,
                    Email = registerViewModel.Email,

                };
                var result = await userManager.CreateAsync(user, registerViewModel.Password);
                if (result.Succeeded)
                    return View("Login");
            }
            return View(registerViewModel);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if(ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(loginViewModel.Email);
                if(user is not null)
                {
                    var result = await signInManager.PasswordSignInAsync(user, loginViewModel.Password, loginViewModel.RememberMe, false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Admin");
                    }
             
                }
            }
            ModelState.AddModelError("", "Invalid login attempt");
            return View(loginViewModel);
        }


    }
}
