using DataAccess.Abstractions;

namespace DataAccess.Models;

public class Book : EntityWithId<Guid>
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Image { get; set; } = string.Empty;
    public int PagesCount { get; set; }
    public int Year { get; set; }
    public double Rating { get; set; }
    public ICollection<Review> Reviews { get; set; }
    public ICollection<BookGenre> BooksGenres { get; set; }
    public ICollection<BookAuthor> BooksAuthors { get; set; }
    public ICollection<BookCollection> BooksCollections { get; set; }
}