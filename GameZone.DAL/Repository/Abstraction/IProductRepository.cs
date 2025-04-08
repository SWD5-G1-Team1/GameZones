using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game.DAL.Entities;

namespace Game.DAL.Repository.Abstraction
{
    public interface IProductRepo : IGenericRepository<Product>
    {
        List<Product> GetProductsByCategory(long categoryId);
    }
}
