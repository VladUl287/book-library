using DataAccess.Abstractions;

namespace DataAccess.Models;

public class Bookmark : EntityWithId<Guid>
{
    public Guid UserId { get; set; }
    public User User { get; set; }
    public Guid BookId { get; set; }
    public Book Book { get; set; }
}