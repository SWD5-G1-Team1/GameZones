using Bl.Services.Abstraction;
using Game.BL.Services.Abstraction;
using Game.BL.Services.Implementation;
using Game.DAL.Entities;
using Game.DAL.Repository.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bl.Services.Implementation
{
    public class CategoryServices: GenericService<Category>, ICategoryServices
    {
        public CategoryServices(IGenericRepository<Category> repository) : base(repository)
        {
        }
    
    }
}
