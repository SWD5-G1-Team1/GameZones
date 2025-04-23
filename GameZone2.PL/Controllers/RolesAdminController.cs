using GameZone2.PL.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GameZone2.PL.Controllers
{
    public class RolesAdminController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;

        public RolesAdminController(RoleManager<IdentityRole> roleManager)
        {
            this.roleManager = roleManager;
        }

        public async Task<IActionResult> Index(string Search)
        {
            var roleQuery = roleManager.Roles.AsQueryable();
            if (!string.IsNullOrEmpty(Search))
            {
                roleQuery = roleQuery.Where(u => u.Name.ToLower().Contains(Search.ToLower()));
            }
            var roleList = await roleQuery.Select(r => new RoleAdminViewModel()
            {
                Id = r.Id,
                Name = r.Name
            }).ToListAsync();
            
            return View(roleList);

        }
        [HttpPost]
        public async Task<ActionResult> Delete(string id)
        {


            if (id is null)
                return BadRequest();
            var user = await roleManager.FindByIdAsync(id);
            if (user is null)
                return NotFound();
            var result = await roleManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                return new StatusCodeResult(200);
            }
            return View("error");

        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();

        }
        [HttpPost]
        public async Task<IActionResult> Create(RoleAdminViewModel roleAdminViewModel)
        {
            if(ModelState.IsValid)
            {
                var result = await roleManager.CreateAsync(new IdentityRole()
                {
                    Name = roleAdminViewModel.Name
                });
                if(result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                return View("error");

            }
            return View(roleAdminViewModel);

        }
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            if (id is null)
                return BadRequest();
            var role = await roleManager.FindByIdAsync(id);
            if (role is null)
                return NotFound();
            var roleModel = new RoleAdminViewModel()
            {
                Id = role.Id,
                Name = role.Name
            };

            return View(roleModel);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(RoleAdminViewModel roleAdminViewModel)
        {
           if(ModelState.IsValid)
            {
                var role = await roleManager.FindByIdAsync(roleAdminViewModel.Id);
                if (role is null)
                    return NotFound();
                role.Name = roleAdminViewModel.Name;
                var result =await roleManager.UpdateAsync(role);
                if (result.Succeeded)
                    return RedirectToAction("Index");
                else
                    return View("error");
            }
            return View(roleAdminViewModel);
        }
    }
}
