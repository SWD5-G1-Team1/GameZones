using Microsoft.CodeAnalysis.Elfie.Model.Strings;

namespace GameZone2.PL.Models.Products
{
    public class ProductViewModel
    {
        public string ProductName { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int CategoryId { get; set; }
        public int StockQuantity { get; set; }
        public IFormFile Image { get; set; }
        

    }
}
