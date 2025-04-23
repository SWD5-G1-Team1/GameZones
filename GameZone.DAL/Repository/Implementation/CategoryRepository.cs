using Game.DAL.DataBase;
using Game.DAL.Entities;
using Game.DAL.Repository.Abstraction;
using Game.DAL.Repository.Implementation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository.Implementation
{
    public class CategoryRepository:GenericRepository<Category>, ICategoryReposratory
    {
        private readonly ApplicationDbContext _context;

    // ✅ استقبل الـ DbContext كمُعامل بدلاً من إنشائه بنفسك
    public CategoryRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }
    
    }
}
