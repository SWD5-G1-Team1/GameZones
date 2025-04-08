using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.DAL.Entities
{
    public class ProductCart
    {
        public long ProductId { get; set; }
        public Product Product { get; set; }
        public long CartId { get; set; }
        public Cart Cart { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
