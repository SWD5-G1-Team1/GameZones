using Bl.Services.Implementation;
using Game.DAL.Entities;
using GameZone2.PL.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace GameZone2.PL.Controllers
{
    public class UsersAdminController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;

        public UsersAdminController(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        public async Task<IActionResult> Index(string Search)
        {
            var userQuery = userManager.Users.AsQueryable();
            if(!string.IsNullOrEmpty(Search))
            {
                userQuery = userQuery.Where(u => u.FullName.ToLower().Contains(Search.ToLower()));
            }
            var userList =await userQuery.Select(u => new UserAdminViewModel()
            {
                Id = u.Id,
                FullName = u.FullName,
                Email = u.Email
            }).ToListAsync();
            foreach(var user in userList)
            {
                user.Roles = await userManager.GetRolesAsync(await userManager.FindByIdAsync(user.Id));
            }
            return View(userList);
            
        }
        [HttpPost]
        public async Task<ActionResult> Delete(string id)
        {


            if (id is null)
                return BadRequest();
            var user = await userManager.FindByIdAsync(id);
            if (user is null)
                return NotFound();
            var result = await userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                return new StatusCodeResult(200);
            }
            return View("error");

        }
    }
}
