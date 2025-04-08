using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.DAL.Entities
{
    public class Payment:BaseClass
    {
        public long Id { get; set; }
        public DateTime PaymentDate { get; set; }
        public string PaymentStatus { get; set; }
        public decimal Amount { get; set; }
        public string PaymentMethod { get; set; }
        public long ?OrderId { get; set; }
        public Order Order { get; set; }
    }
}
