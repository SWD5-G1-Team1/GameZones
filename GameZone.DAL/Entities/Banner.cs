using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.DAL.Entities
{
    public class Banner
    {
        public long Id { get; set; }
        public string Image { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Category Category { get; set; }
        public long? CategoryId {  get; set; }
        public List<Product>Products { get; set; }
    }
}
