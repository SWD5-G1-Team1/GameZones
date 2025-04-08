using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game.BL.Services.Abstraction;
using Game.DAL.Entities;
using Game.DAL.Repository.Abstraction;


namespace Game.BL.Services.Implementation
{
    public class ProductService : GenericService<Product>, IProductService
    {
        public ProductService(IGenericRepository<Product> repository) : base(repository)
        {
        }
    }
}
