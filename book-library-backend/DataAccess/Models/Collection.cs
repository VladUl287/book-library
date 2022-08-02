using DataAccess.Abstractions;

namespace DataAccess.Models;

public class Collection : EntityWithId<Guid>
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Image { get; set; } = string.Empty;
    public int Likes { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; }
    public DateTime DateCreate { get; set; }
    public ICollection<BookCollection> BooksCollections { get; set; } = Array.Empty<BookCollection>();
}