using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Game.DAL.Repository.Abstraction
{
    public interface IGenericRepository<T> where T : class
    {
        (bool, string?) Create(T entity);
        (bool, string?) Edit(T entity, long id);
        bool IsDeleted(long id);
        T Get(Expression<Func<T, bool>>? filter = null);
        List<T> GetAll(Expression<Func<T, bool>>? filter = null);
    }
}
