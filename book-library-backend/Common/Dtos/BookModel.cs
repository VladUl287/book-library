namespace Common.Dtos;

public sealed class BookModel
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Image { get; set; } = string.Empty;
    public bool Bookmark { get; set; }
    public int PagesCount { get; set; }
    public IEnumerable<AuthorModel> Authors { get; set; } = Array.Empty<AuthorModel>();
}