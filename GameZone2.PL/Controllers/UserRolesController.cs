using Game.DAL.Entities;
using GameZone2.PL.Models.Roles;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GameZone2.PL.Controllers
{
    public class UserRolesController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserRolesController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpGet]
        public async Task<IActionResult> Assign(string username = "")
        {
            // جلب المستخدمين بناءً على الاسم المدخل
            var users = await _userManager.Users
                .Where(u => u.FullName.ToLower().Contains(username.ToLower()))
                .ToListAsync();
            var roles = await _roleManager.Roles.ToListAsync();
            var result = new List<AssignRolesViewModel>();
            foreach (var user in users)
            {
                result.Add(new AssignRolesViewModel()
                {
                    Username = user.FullName,
                    Roles = roles.Select(r => new RoleCheckbox()
                    {
                        RoleName = r.Name,
                        IsSelected = _userManager.IsInRoleAsync(user, r.Name).Result

                    })
                }
                );


            }
            return View(result);


        }



        [HttpPost]
        public async Task<IActionResult> Assign(AssignRolesViewModel model)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.FullName == model.Username);

            if (user == null)
            {
                ModelState.AddModelError("", "User not found.");
                return View(new List<AssignRolesViewModel> { model });
            }

            var existingRoles = await _userManager.GetRolesAsync(user);
            await _userManager.RemoveFromRolesAsync(user, existingRoles);

            var selectedRoles = model.Roles.Where(r => r.IsSelected).Select(r => r.RoleName).ToList();
            await _userManager.AddToRolesAsync(user, selectedRoles);

            // إعادة تحميل كل المستخدمين مع الأدوار
            var users = await _userManager.Users.ToListAsync();
            var roles = await _roleManager.Roles.ToListAsync();
            var result = new List<AssignRolesViewModel>();

            foreach (var u in users)
            {
                var assignModel = new AssignRolesViewModel
                {
                    Username = u.FullName,
                    Roles = roles.Select(r => new RoleCheckbox
                    {
                        RoleName = r.Name,
                        IsSelected = _userManager.IsInRoleAsync(u, r.Name).Result
                    }),
                    Message = u.FullName == model.Username? $"✅ Assigned roles to <span class='fw-bold text-dark'>{u.UserName}</span>: <span class='text-primary'>{string.Join(", ", selectedRoles)}</span>": null
                };

                result.Add(assignModel);
            }

            return View(result);
        }
    }
}
