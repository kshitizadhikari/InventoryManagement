using Domain.Enums;

namespace Domain.Entities
{
    public class RoleAssignment
    {
        public string Email { get; set; }
        public Role Role { get; set; }
    }
}
