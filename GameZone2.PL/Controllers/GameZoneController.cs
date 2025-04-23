using Game.DAL.DataBase;
using Microsoft.AspNetCore.Mvc;
using DAL.ViewModels;
using Microsoft.EntityFrameworkCore;
namespace GameZone2.PL.Controllers
{
    public class GameZoneController : Controller
    {
        private readonly ApplicationDbContext _context;


        public GameZoneController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var viewModel = new HomePageVM
            {
                Categories = _context.Categories.ToList(),
                 banners = _context.Banners
                              .Include(b => b.Category)
                              .Include(b => b.Products)
                              .ToList(),
            };

            return View(viewModel);
        }
    }
}
