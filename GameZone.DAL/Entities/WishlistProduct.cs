using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.DAL.Entities
{
    public class WishlistProduct
    {
        public long ProductId { get; set; }
        public Product Product { get; set; }
        public long WishlistId { get; set; }
        public WishList WishList { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
