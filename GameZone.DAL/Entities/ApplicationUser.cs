using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Game.DAL.Entities
{
    public enum UserType
{
    Admin = 1,
    Seller = 2,
    Customer = 3
}
    public class ApplicationUser:IdentityUser
    {
        public string FullName { get; set; }
        public string UserType { get; set; }
        public List<Product> Products { get; set; }
        public List<Order> Orders { get; set; }
        public List<Notification> Notifications { get; set; }
        public WishList WishList { get; set; }
        public Cart Cart { get; set; }
    }
}
