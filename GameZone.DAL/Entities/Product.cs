
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Game.DAL.Entities
{
    public class Product:BaseClass
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string ImagePath { get; set; }
        public int StockQuantity { get; set; }
        //---------------Navigation Property------------------//
        //  المفتاح الأجنبي لربط المنتج بالفئة
        public long? CategoryId { get; set; }
        //  العلاقة مع الفئة
        public  Category Category { get;  set; }
        //  المفتاح الأجنبي لربط المنتج بالخصم
        public long? DiscountId { get; set; }
        //  العلاقة مع الخصم
        public Discount Discount { get; set; }
        //  المفتاح الأجنبي لربط المنتج بالمستخدم
        public string? SellerId { get; set; }
        //  العلاقة مع المستخدم
        public ApplicationUser Seller  { get; set; }
        public List<ProductOrder> ProductOrders { get; set; }
        public List<ProductCart> productCarts { get; set; }
        public List<WishlistProduct> wishlistProducts { get; set;}
        public Banner Banner { get; set; }
        public long? BannerId {  get; set; }
        
    }

}
