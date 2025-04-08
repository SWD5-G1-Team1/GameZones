using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.DAL.Entities
{
    public class ProductOrder
    {
        public long ProductId { get; set; }
        public long OrderId { get; set; }  // ✅ يجب أن يكون Nullable

        public Product Product { get; set; }
        public Order Order { get; set; }
        public int Quantity { get; set; }
        public decimal Price {get; set;}
       
    }
}
