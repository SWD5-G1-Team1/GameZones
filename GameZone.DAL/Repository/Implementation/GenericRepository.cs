using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Game.DAL.DataBase;
using Game.DAL.Repository.Abstraction;
using Microsoft.EntityFrameworkCore;

namespace Game.DAL.Repository.Implementation
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;

        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public (bool, string?) Create(T entity)
        {
            try
            {
                _context.Add(entity);
                _context.SaveChanges();
                return (true, null);
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        public (bool, string?) Edit(T entity, long id)
        {
            try
            {
                var existingEntity = _context.Find<T>(id);
                if (existingEntity == null)
                    return (false, "Entity Not Found");

                _context.Entry(existingEntity).CurrentValues.SetValues(entity);
                _context.SaveChanges();
                return (true, null);
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        public bool IsDeleted(long id)
        {
            try
            {
                var entity = _context.Find<T>(id);
                if (entity == null)
                    return false;

                _context.Remove(entity);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public T Get(Expression<Func<T, bool>>? filter = null)
        {
            return filter == null ? _context.Set<T>().FirstOrDefault() : _context.Set<T>().FirstOrDefault(filter);
        }

        public List<T> GetAll(Expression<Func<T, bool>>? filter = null)
        {
            return filter == null ? _context.Set<T>().ToList() : _context.Set<T>().Where(filter).ToList();
        }
    }
}

