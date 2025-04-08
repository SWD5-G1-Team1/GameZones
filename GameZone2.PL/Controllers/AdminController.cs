using Game.BL.Services.Abstraction;
using Microsoft.AspNetCore.Mvc;

namespace Game.PL.Controllers
{
    public class AdminController : Controller
    {
        
        public IActionResult Index()
        {
            ViewData["Title"] = "Dashboard";
            return View();
        }
    }
}
