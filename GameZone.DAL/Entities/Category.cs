namespace Game.DAL.Entities
{
    public class Category:BaseClass
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        // 🔹 العلاقة مع المنتجات
        public List<Product> Products { get;  set; }
        public List<Banner> Banners { get;  set; }
       
     
    }
}