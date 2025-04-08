using Microsoft.AspNetCore.Mvc;

namespace Game.PL.Controllers
{
    public class OrderController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
