using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Game.DAL.Entities;
using System.Reflection;

namespace Game.DAL.DataBase
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Discount> Discounts { get; set; }
        public DbSet<Order> orders { get; set; }
        public DbSet<ProductOrder> ProductOrders { get; set; }
        public DbSet<WishList> WishLists { get; set; }
        public DbSet<WishlistProduct> WishlistProducts { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<ProductCart> ProductCarts { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Banner> Banners { get; set; }
        public DbSet<Payment> Payments { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ApplicationUser>(entity =>
            {
                entity.ToTable("Users");

                entity.Property(u => u.FullName)
                    .IsRequired()
                    .HasMaxLength(200);
                entity.Property(u => u.UserType)
                  .HasConversion<string>(); // يحفظ القيم كـ "Customer", "Seller", "Admin"
                // 🔹 العلاقات مع الجداول الأخرى
                entity.HasMany(u => u.Products)
                 .WithOne(p => p.Seller)
                 .HasForeignKey(p => p.SellerId)
                 .OnDelete(DeleteBehavior.SetNull);
                entity.HasMany(u => u.Orders)
                    .WithOne(o => o.User)
                    .HasForeignKey(o => o.UserId)
                    .OnDelete(DeleteBehavior.SetNull);

                entity.HasMany(u => u.Notifications)
                    .WithOne(n => n.User)
                    .HasForeignKey(n => n.UserId)
                    .OnDelete(DeleteBehavior.SetNull);

                entity.HasOne(u => u.Cart)
                    .WithOne(c => c.User)
                    .HasForeignKey<Cart>(c => c.UserId)
                    .OnDelete(DeleteBehavior.SetNull);

                entity.HasOne(u => u.WishList)
                    .WithOne(w => w.User)
                    .HasForeignKey<WishList>(w => w.UserId)
                    .OnDelete(DeleteBehavior.SetNull);
            });
            // 🔹 إعدادات Product

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(p => p.Id); // تحديد المفتاح الأساسي

                entity.Property(p => p.Name)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(p => p.Description)
                    .HasMaxLength(1000);

                entity.Property(p => p.Price)
                    .HasColumnType("decimal(18,2)")
                    .IsRequired();
                entity.Property(p => p.ImagePath)
                    .HasMaxLength(500);
                // 🔹 علاقة المنتج مع البائع (User) (Many-to-One)
                entity.HasOne(p => p.Seller)
                    .WithMany(u => u.Products)
                    .HasForeignKey(p => p.SellerId)
                    .OnDelete(DeleteBehavior.SetNull);

                // 🔹 علاقة المنتج مع الفئة (Category) (Many-to-One)
                entity.HasOne(p => p.Category)
                    .WithMany(c => c.Products)
                    .HasForeignKey(p => p.CategoryId)
                    .OnDelete(DeleteBehavior.SetNull);

                // 🔹 علاقة المنتج مع الـ Banner (Many-to-One)
                entity.HasOne(p => p.Banner)
                    .WithMany(b => b.Products)
                    .HasForeignKey(p => p.BannerId)
                    .OnDelete(DeleteBehavior.SetNull);

                // 🔹 علاقة المنتج مع الطلبات (Many-to-Many) عبر OrderProduct
                entity.HasMany(p => p.ProductOrders)
                    .WithOne(op => op.Product)
                    .HasForeignKey(op => op.ProductId)
                    .OnDelete(DeleteBehavior.SetNull);

                // 🔹 علاقة المنتج مع العربة (Many-to-Many) عبر CartItem
                entity.HasMany(p => p.productCarts)
                    .WithOne(ci => ci.Product)
                    .HasForeignKey(ci => ci.ProductId)
                    .OnDelete(DeleteBehavior.SetNull);
                // 🔹 علاقة المنتج مع المفضله (Many-to-Many) عبر CartItem

                entity.HasMany(p => p.wishlistProducts)
                  .WithOne(ci => ci.Product)
                  .HasForeignKey(ci => ci.ProductId)
                  .OnDelete(DeleteBehavior.SetNull);
            });
        

            // 🔹 إعدادات Category

            modelBuilder.Entity<Category>(entity =>
                {
                    entity.ToTable("Categories");

                    // 🔹 المفتاح الأساسي
                    entity.HasKey(c => c.Id);
                    entity.HasKey(c => c.Id);
                    entity.Property(c => c.Name)
                          .IsRequired()
                          .HasMaxLength(255);
                    entity.HasIndex(c => c.Name)
                          .IsUnique(); // كل فئة يجب أن يكون لها اسم فريد

                    // 🔹 العلاقة مع الإعلانات (1:M)
                    entity.HasMany(c => c.Banners)
                        .WithOne(b => b.Category)
                        .HasForeignKey(b => b.CategoryId)
                        .OnDelete(DeleteBehavior.SetNull)
                        .HasConstraintName("FK_Banners_Categories");
                    entity.HasMany(c => c.Products)
                     .WithOne(b => b.Category)
                     .HasForeignKey(b => b.CategoryId)
                     .OnDelete(DeleteBehavior.SetNull)
                     .HasConstraintName("FK_Banners_Categories");

                });
            // جدول الخصومات - علاقة 1 إلى 1
            // 🔹 جدول Discount
            modelBuilder.Entity<Discount>(entity =>
            {
                entity.ToTable("Discounts");

                entity.HasKey(d => d.Id);

                entity.Property(d => d.DiscountValue)
                    .IsRequired()
                    .HasColumnType("decimal(5,2)")
                    .HasComment("نسبة الخصم");

                entity.Property(d => d.StartDate)
                    .IsRequired()
                    .HasColumnType("datetime")
                    .HasComment("تاريخ بدء الخصم");

                entity.Property(d => d.EndDate)
                    .IsRequired()
                    .HasColumnType("datetime")
                    .HasComment("تاريخ انتهاء الخصم");

                // 🔹 العلاقة 1:1 مع Product
                entity.HasOne(d => d.Product)
                    .WithOne(p => p.Discount)
                    .HasForeignKey<Discount>(d => d.ProductId) // ProductId هو الـ FK في جدول Discount
                    .OnDelete(DeleteBehavior.SetNull)  // عند حذف المنتج، يتم حذف الخصم أيضًا
                    .HasConstraintName("FK_Discount_Product");
            });
            //جدول الطلب
            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("Orders");

                entity.HasKey(o => o.Id);
                // 🔹 تخزين OrderStatus كنص والتأكد أنه ليس NULL
                entity.Property(o => o.OrderStatus)
                    .IsRequired()
                    .HasMaxLength(50);  // تحديد الطول الأقصى

                // 🔹 تخزين PaymentStatus كنص والتأكد أنه ليس NULL
                entity.Property(o => o.PaymentStatus)
                    .IsRequired()
                    .HasMaxLength(50);
  
                    // 🔹 علاقة الطلب مع المستخدم (Many-to-One)
                    entity.HasOne(o => o.User)
                        .WithMany(u => u.Orders)
                        .HasForeignKey(o => o.UserId)
                        .OnDelete(DeleteBehavior.SetNull); // حذف الطلبات عند حذف المستخدم

                    // 🔹 علاقة الطلب مع المنتجات (Many-to-Many) عبر OrderProduct
                    entity.HasMany(o => o.ProductOrders)
                        .WithOne(op => op.Order)
                        .HasForeignKey(op => op.OrderId)
                        .OnDelete(DeleteBehavior.SetNull);
                });


           
            // 🔥 تعريف العلاقة Many-to-Many بين Orders و Products
            modelBuilder.Entity<ProductOrder>(entity =>
            {
                entity.ToTable("ProductsOrders");

                // 🔹 تحديد المفتاح المركب (ProductId, OrderId)
                entity.HasKey(po => new { po.ProductId, po.OrderId });

                // 🔹 العلاقة مع Product
                entity.HasOne(po => po.Product)
                    .WithMany(p => p.ProductOrders)
                    .HasForeignKey(po => po.ProductId)
                    .OnDelete(DeleteBehavior.NoAction);

                // 🔹 العلاقة مع Order
                entity.HasOne(po => po.Order)
                    .WithMany(o => o.ProductOrders)
                    .HasForeignKey(po => po.OrderId)
                    .OnDelete(DeleteBehavior.NoAction);

                // 🔹 ضبط Quantity
                entity.Property(po => po.Quantity)
                    .IsRequired()
                    .HasDefaultValue(1)  // القيمة الافتراضية 1
                    .HasColumnType("int")
                    .HasComment("عدد المنتجات المطلوبة");

                // 🔹 ضبط Price
                entity.Property(po => po.Price)
                    .IsRequired()
                    .HasColumnType("decimal(18,2)")
                    .HasComment("السعر النهائي للمنتج عند الشراء");

            });


            modelBuilder.Entity<Cart>(entity =>
            {
                entity.ToTable("Cart");

                entity.HasKey(o => o.Id);
                // 🔹 تخزين OrderStatus كنص والتأكد أنه ليس NULL
                entity.Property(o => o.CartStatus)
                    .IsRequired()
                    .HasMaxLength(50);  // تحديد الطول الأقصى
            });

            modelBuilder.Entity<ProductCart>(entity =>
            {
                entity.ToTable("ProductCart");

                // 🔹 تحديد المفتاح الأساسي المركب
                entity.HasKey(pc => new { pc.CartId, pc.ProductId });

                // 🔹 العلاقة مع Cart
                entity.HasOne(pc => pc.Cart)
                    .WithMany(c => c.productCarts)
                    .HasForeignKey(pc => pc.CartId)
                    .OnDelete(DeleteBehavior.NoAction); // عند حذف Cart، يتم حذف السجلات المرتبطة

                // 🔹 العلاقة مع Product
                entity.HasOne(pc => pc.Product)
                    .WithMany(p => p.productCarts)
                    .HasForeignKey(pc => pc.ProductId)
                    .OnDelete(DeleteBehavior.NoAction); // عند حذف Product، يتم حذف السجلات المرتبطة

                // 🔹 ضبط Quantity
                entity.Property(pc => pc.Quantity)
                    .IsRequired()
                    .HasDefaultValue(1)
                    .HasColumnType("int")
                    .HasComment("عدد المنتجات في السلة");

                // 🔹 ضبط Price
                entity.Property(pc => pc.Price)
                    .IsRequired()
                    .HasColumnType("decimal(18,2)")
                    .HasComment("السعر النهائي للمنتج في السلة");
            });
            modelBuilder.Entity<WishList>(entity =>
            {
                entity.ToTable("WishLists");

                // 🔹 المفتاح الأساسي
                entity.HasKey(w => w.Id);

                // 🔹 اسم قائمة الأمنيات
                entity.Property(w => w.WishlistStatus)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasComment("اسم قائمة الأمنيات");


            });
            modelBuilder.Entity<WishlistProduct>(entity =>
            {
                entity.ToTable("WishListProducts");

                // 🔹 تحديد المفتاح الأساسي المركب
                entity.HasKey(wp => new { wp.WishlistId, wp.ProductId });

                // 🔹 العلاقة بين WishListProduct و WishList
                entity.HasOne(wp => wp.WishList)
                    .WithMany(w => w.WishlistProducts)
                    .HasForeignKey(wp => wp.WishlistId)
                    .OnDelete(DeleteBehavior.NoAction)
                    .HasConstraintName("FK_WishListProducts_WishLists");

                // 🔹 العلاقة بين WishListProduct و Product
                entity.HasOne(wp => wp.Product)
                    .WithMany(p => p.wishlistProducts)
                    .HasForeignKey(wp => wp.ProductId)
                    .OnDelete(DeleteBehavior.NoAction)
                    .HasConstraintName("FK_WishListProducts_Products");
                entity.Property(pc => pc.Price)
                 .IsRequired()
                 .HasColumnType("decimal(18,2)")
                 .HasComment("السعر النهائي للمنتج في السلة");

            });
            modelBuilder.Entity<Payment>(static entity =>
            {
                entity.ToTable("Payments");

                // تحديد المفتاح الأساسي
                entity.HasKey(p => p.Id);

                // ضبط خاصية قيمة الدفع
                entity.Property(p => p.Amount)
                      .IsRequired()
                      .HasColumnType("decimal(18,2)")
                      .HasComment("قيمة الدفع");

                // ضبط خاصية تاريخ الدفع مع القيمة الافتراضية
                entity.Property(p => p.PaymentDate)
                      .IsRequired()
                      .HasColumnType("datetime")
                      .HasDefaultValueSql("GETDATE()")
                      .HasComment("تاريخ الدفع");

                // ضبط خاصية حالة الدفع كـ string مع تحديد الحد الأقصى للطول
                entity.Property(p => p.PaymentStatus)
                      .IsRequired()
                      .HasMaxLength(50)
                      .HasColumnType("nvarchar(50)")
                      .HasComment("حالة الدفع، مثل: Pending, Completed, Failed, Refunded");

                // مثال على علاقة الدفع مع الطلب (1:1)
                // نفترض أن كل عملية دفع مرتبطة بطلب واحد فقط
                entity.HasOne(p => p.Order)
                      .WithOne(o => o.Payment)
                      .HasForeignKey<Payment>(p => p.OrderId)
                      .OnDelete(DeleteBehavior.SetNull)  // عند حذف الطلب، يتم حذف الدفع المرتبط به
                      .HasConstraintName("FK_Payments_Orders");
            });
            modelBuilder.Entity<Notification>(entity =>
            {
                entity.ToTable("Notifications");

                // المفتاح الأساسي
                entity.HasKey(n => n.Id);

                // نص الإشعار
                entity.Property(n => n.Message)
                    .IsRequired()
                    .HasMaxLength(1000)
                    .HasColumnType("nvarchar(1000)")
                    .HasComment("نص الإشعار");

                // نوع الإشعار (مثل: Info, Warning, Error)
                entity.Property(n => n.Type)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnType("nvarchar(100)")
                    .HasComment("نوع الإشعار");

                //تاريخ إنشاء الإشعار مع القيمة الافتراضية
                entity.Property(n => n.CreatedOn)
                    .IsRequired()
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("GETDATE()")
                    .HasComment("تاريخ إنشاء الإشعار");

                entity.Property(n => n.UserId)
                    .IsRequired(false);

                entity.HasOne(n => n.User)
                    .WithMany(u => u.Notifications)
                    .HasForeignKey(n => n.UserId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_Notifications_Users");
            });


            modelBuilder.Entity<Banner>(entity =>
            {
                entity.ToTable("Banners");

                // 🔹 المفتاح الأساسي
                entity.HasKey(b => b.Id);

                // 🔹 صورة الإعلان
                entity.Property(b => b.Image)
                    .IsRequired()
                    .HasMaxLength(500)
                    .HasColumnType("nvarchar(500)")
                    .HasComment("مسار صورة الإعلان");

                // 🔹 تاريخ البداية
                entity.Property(b => b.StartDate)
                    .IsRequired()
                    .HasColumnType("datetime")
                    .HasComment("تاريخ بداية الإعلان");

                // 🔹 تاريخ النهاية
                entity.Property(b => b.EndDate)
                    .IsRequired()
                    .HasColumnType("datetime")
                    .HasComment("تاريخ نهاية الإعلان");

                // 🔹 العلاقة مع الفئة (Category) → (M:1)
                entity.HasOne(b => b.Category)
                    .WithMany(c => c.Banners)
                    .HasForeignKey(b => b.CategoryId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_Banner_Categories");

                // 🔹 العلاقة مع المنتجات (Product) → (1:M)
                entity.HasMany(b => b.Products)
                    .WithOne(p => p.Banner)
                    .HasForeignKey(p => p.BannerId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_Products_Banners");
            });


        }
    }
}
    
