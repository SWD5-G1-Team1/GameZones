namespace GameZone2.PL.Models.Identity
{
    public class RoleAdminViewModel
    {
        public RoleAdminViewModel()
        {
            Id = Guid.NewGuid().ToString();
        }

        public string Id { get; set; }
        public string Name { get; set; }

    }
}
