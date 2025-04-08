namespace Game.DAL.Entities
{
    public class WishList:BaseClass
    {
        public long Id { get; set; }
        public string WishlistStatus { get; set; }
        public List <WishlistProduct> WishlistProducts { get; set; }
        public string? UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}
