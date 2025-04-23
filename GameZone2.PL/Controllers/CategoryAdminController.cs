using Bl.Services.Abstraction;
using GameZone2.PL.Models.Category;
using Microsoft.AspNetCore.Mvc;

namespace Game.PL.Controllers
{
    public class CategoryAdminController : Controller
    {
        private readonly ICategoryServices categoryServices;

        public CategoryAdminController(ICategoryServices categoryServices)

        {
            this.categoryServices = categoryServices;
        }

        public IActionResult Index()
        {
            var categories = categoryServices.GetAll().Select(category => new CategoryViewModel()
            {
                Id = category.Id,
                CategoryName = category.Name,
                Description = category.Description
            }).ToList();

            return View(categories);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(CategoryViewModel categoryViewModel)
        {
            if (!ModelState.IsValid)
                return View(categoryViewModel);
            try
            {
                categoryServices.Create(new DAL.Entities.Category()
                {
                    Name = categoryViewModel.CategoryName,
                    Description = categoryViewModel.Description
                });
                return RedirectToAction("Index");
            }
            catch
            {
                return View("error");
            }

        }
        [HttpPost]
        public ActionResult Delete(long id)
        {
            

            var result = categoryServices.IsDeleted(id);
            if (result)
            {
                return new StatusCodeResult(200);
            }
            return View("error");

        }
        [HttpGet]
        public IActionResult Edit(long? id)
        {
            if (id == null)
                return BadRequest();
            var (category,Success,Massage) = categoryServices.GetById(id.Value);
            if (category is null)
                return NotFound();
            return View(new CategoryViewModel()
            {
                Id = category.Id,
                CategoryName = category.Name,
                Description = category.Description
            });
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Edit(long? id,CategoryViewModel categoryViewModel)
        {
            if (id == null)
                return BadRequest();
            var (category, Success, Massage) = categoryServices.GetById(id.Value);
            if (category is null)
                return NotFound();
            category.Name = categoryViewModel.CategoryName;
            category.Description = categoryViewModel.Description;
            var (Succes, Massag) = categoryServices.Edit(category,category.Id);
            if (Succes)
                return RedirectToAction("Index");
            else
                return View("error");
             
        }
    }
}
