using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.DAL.Entities
{
  
    public class Order:BaseClass
    {
        public long Id { get; set; }
        public string OrderStatus { get; set; }
        public string PaymentStatus { get; set; }
        public Payment Payment { get; set; }
        public List<ProductOrder> ProductOrders { get; set; }
        public string? UserId { get; set; }
        public ApplicationUser User { get; set; }


    }
}
