using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.DAL.Entities
{
    public class Cart:BaseClass
    {
        public long Id {  get; set; }
        public string CartStatus { get; set; }
        public List<ProductCart> productCarts { get; set; }
        public string? UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}
