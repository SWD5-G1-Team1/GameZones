using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Game.BL.Services.Abstraction;
using Game.DAL.Repository.Abstraction;
using Microsoft.EntityFrameworkCore;

namespace Game.BL.Services.Implementation
{
    public class GenericService<T> : IGenericService<T> where T : class
    {
        private readonly IGenericRepository<T> _repository;

        public GenericService(IGenericRepository<T> repository)
        {
            _repository = repository;
        }

        public (bool, string?) Create(T entity)
        {
            return _repository.Create(entity);
        }

        public (bool, string?) Edit(T entity, long id)
        {
            return _repository.Edit(entity, id);
        }

        public bool IsDeleted(long id)
        {
            return _repository.IsDeleted(id);
        }

        public List<T> GetAll()
        {
            return _repository.GetAll();
        }

        public (T?, bool, string?) GetById(long id)
        {
            try
            {
                var result = _repository.Get(e => (long)e!.GetType().GetProperty("Id")!.GetValue(e)! == id);
                if (result == null)
                    return (null, false, "Entity not found");

                return (result, true, null);
            }
            catch (Exception ex)
            {
                return (null, false, ex.Message);
            }
        }
    }


}
