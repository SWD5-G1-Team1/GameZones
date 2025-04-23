namespace GameZone2.PL.Models.Roles
{
    public class AssignRolesViewModel
    {
        public string Username { get; set; }
        public IEnumerable<RoleCheckbox> Roles { get; set; } = new List<RoleCheckbox>();
        public string Message { get; set; }
    }
}
