using Game.DAL.Entities;
using GameZone2.PL.Models.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
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
        private async Task<IActionResult> RedirectUserByRole(ApplicationUser user)
        {
            //var roles = await userManager.GetRolesAsync(user);

            //if (roles.Contains("Admin"))
            //    return RedirectToAction("Index", "Admin");

            return RedirectToAction("Index", "GameZone");
        }


        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        
        public async Task<IActionResult> SignOut()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "GameZone");
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(loginViewModel.Email);
                if (user is not null)
                {
                    var result = await signInManager.PasswordSignInAsync(user, loginViewModel.Password, loginViewModel.RememberMe, false);

                    if (result.Succeeded)
                    {
                        // ⬇️ تسجيل الدخول مع Claims جديدة فيها الاسم
                        await signInManager.SignOutAsync(); // لازم نخرج عشان نعيد الدخول بالـ claims الجديدة

                        var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
                        identity.AddClaim(new Claim(ClaimTypes.Name, user.FullName ?? user.UserName)); // لو عندك FullName
                        identity.AddClaim(new Claim(ClaimTypes.Email, user.Email));
                        identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id));
                        await signInManager.SignInAsync(user, isPersistent: false);
                        return await RedirectUserByRole(user); // تحويل حسب الـ Role
                    }
                }
            }

            ModelState.AddModelError("", "Invalid login attempt");
            return View(loginViewModel);
        }



    }
}
