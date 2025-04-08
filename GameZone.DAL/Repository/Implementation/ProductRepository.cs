using System.Collections.Generic;
using System.Linq;
using Game.DAL.DataBase;
using Game.DAL.Entities;
using Game.DAL.Repository.Abstraction;
using Microsoft.EntityFrameworkCore;

namespace Game.DAL.Repository.Implementation
{
    public class ProductRepo : GenericRepository<Product>, IProductRepo
    {
        private readonly ApplicationDbContext _context;

        // ✅ استقبل الـ DbContext كمُعامل بدلاً من إنشائه بنفسك
        public ProductRepo(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public List<Product> GetProductsByCategory(long categoryId)
        {
            return _context.Set<Product>()
                           .Where(p => p.CategoryId == categoryId && !p.IsDeleted)
                           .ToList();
        }
    }
}