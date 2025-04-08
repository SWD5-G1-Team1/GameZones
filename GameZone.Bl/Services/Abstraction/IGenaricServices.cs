using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Game.BL.Services.Abstraction
{
    public interface IGenericService<T> where T : class
    {
        (bool, string?) Create(T entity);
        (bool, string?) Edit(T entity, long id);
        bool IsDeleted(long id);
        List<T> GetAll();
        (T?, bool, string?) GetById(long id);
    }
}
