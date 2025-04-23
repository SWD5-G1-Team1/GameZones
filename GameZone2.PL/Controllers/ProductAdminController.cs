using Game.BL.Services.Abstraction;
using Game.DAL.Entities;
using GameZone2.PL.Models.Helpers;
using GameZone2.PL.Models.Products;
using Microsoft.AspNetCore.Mvc;


namespace Game.PL.Controllers
{
    public class ProductAdminController : Controller
    {
        private readonly IProductService productService;
        private readonly IFileService fileService;

        public ProductAdminController(IProductService productService,IFileService fileService)
        {
            this.productService = productService;
            this.fileService = fileService;
        }

        public IActionResult Index()
        {
            var products=productService.GetAll();
            
            return View(products);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
      
            [HttpPost]
            public async Task<IActionResult> Create(ProductViewModel productViewModel)
            {
                if (!ModelState.IsValid)
                    return View(productViewModel);

            try
                {
                     string imagePath=string.Empty;
                if (productViewModel.Image != null)
                    {
                        // رفع الصورة
                         imagePath = await fileService.SaveImageAsync(productViewModel.Image, "images/products");
                       
                    }

                    // حفظ المنتج
                    productService.Create(new Product() { Name=productViewModel.ProductName,
                    CategoryId=productViewModel.CategoryId,
                    StockQuantity=productViewModel.StockQuantity,
                    Description=productViewModel.Description,
                    ImagePath=imagePath
                    });

                    return RedirectToAction("Index");
                }
                catch (InvalidOperationException ex)
                {
                    ModelState.AddModelError("ImageFile", ex.Message); 
                }
                catch (Exception)
                {
                    ModelState.AddModelError(string.Empty, "حدث خطأ أثناء حفظ المنتج.");
                }

                return View(productViewModel);
            }

       

    }
}
