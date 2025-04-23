using Game.DAL;
using Game.BL.Services.Abstraction;
using Game.BL.Services.Implementation;
using Game.DAL.DataBase;
using Game.DAL.Repository.Abstraction;
using Game.DAL.Repository.Implementation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Game.DAL.Entities;
using Bl.Services.Abstraction;
using Bl.Services.Implementation;
using GameZone2.PL.Models.Helpers;

namespace Game.PL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            // إضافة الخدمات
            builder.Services.AddControllersWithViews();

            // إضافة قاعدة البيانات
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
     options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));



            // ✅ تسجيل الـ DbContext أولًا
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options
                       .UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // ✅ تسجيل الهوية (Identity)
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                
                options.Password.RequireDigit = false;  
                options.Password.RequireLowercase = false;  
                options.Password.RequireUppercase = false;  
                options.Password.RequireNonAlphanumeric = false;  
                options.Password.RequiredLength = 6;  
                options.Password.RequiredUniqueChars = 0;  
            })
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

            // ✅ تسجيل الـ Repositories
            builder.Services.AddScoped<IProductRepo, ProductRepo>();
            builder.Services.AddScoped<IGenericRepository<Product>, GenericRepository<Product>>();
            builder.Services.AddScoped<IGenericRepository<Category>, GenericRepository<Category>>();

            // ✅ تسجيل الـ Services
            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddScoped<ICategoryServices, CategoryServices>();
            builder.Services.AddScoped<IFileService , FileService>();



            WebApplication app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            // ✅ إضافة المصادقة
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=GameZone}/{action=Index}/{id?}");

            // نجعل دالة main غير متزامنة async بحيث نقدر نستخدم await بداخلها

            app.Run();

        }
    }
}