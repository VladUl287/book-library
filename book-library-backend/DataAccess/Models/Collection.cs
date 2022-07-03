using DataAccess.Abstractions;

namespace DataAccess.Models;

public class Collection : EntityWithId<Guid>
{
    public string Description { get; set; } = string.Empty;
    public int Views { get; set; }
    public int Likes { get; set; }
    public ICollection<BookCollection> BooksCollections { get; set; } = Array.Empty<BookCollection>();
}