namespace Game.DAL.Entities
{
    public class BaseClass
    {
        public bool IsDeleted { get; set; } = false;
        public string? CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public DateTime? DeletedOn { get; set; }
        public string? DeletedBy { get; set; }
    }
}
