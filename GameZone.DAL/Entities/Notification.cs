using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.DAL.Entities
{
    public class Notification:BaseClass
    {
        public long Id { get; set; }
        public string Type { get; set; }
        public string Message { get; set; }
        public string? UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}
