using DataAccess.Abstractions;

namespace DataAccess.Models
{
    public class User : EntityWithId<Guid>
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public Guid RoleId { get; set; }
        public Role Role { get; set; }
        public ICollection<Review> Reviews { get; set; }
    }
}