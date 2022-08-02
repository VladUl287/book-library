namespace Domain.Dtos.Abstractions;

public abstract class BookBase
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int PagesCount { get; set; }
    public int Year { get; set; }
    public IEnumerable<AuthorModel> Authors { get; set; } = Array.Empty<AuthorModel>();
    public IEnumerable<GenreModel> Genres { get; set; } = Array.Empty<GenreModel>();
}