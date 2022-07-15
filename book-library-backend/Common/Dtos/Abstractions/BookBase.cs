namespace Common.Dtos.Abstractions;

public abstract class BookBase
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int PagesCount { get; set; }
    public AuthorModel[] Authors { get; set; } = Array.Empty<AuthorModel>();
}