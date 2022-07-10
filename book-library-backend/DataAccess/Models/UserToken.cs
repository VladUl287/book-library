using DataAccess.Abstractions;

namespace DataAccess.Models
{
    public class UserToken : EntityWithId<Guid>
    {
        public Guid UserId { get; set; }
        public User User { get; set; }
        public string RefreshToken { get; set; } = string.Empty;
    }
}