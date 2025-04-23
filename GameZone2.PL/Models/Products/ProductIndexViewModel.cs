namespace GameZone2.PL.Models.Products
{
    public class ProductIndexViewModel
    {
        public long Id { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string category { get; set; }
        public int StockQuantity { get; set; }
        public string ImagePath { get; set; }
    }
}
