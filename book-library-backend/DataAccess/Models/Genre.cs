using DataAccess.Abstractions;

namespace DataAccess.Models;

public class Genre : EntityWithId<Guid>
{
    public string Name { get; set; } = string.Empty;
    public ICollection<BookGenre> BooksGenres { get; set; }
}