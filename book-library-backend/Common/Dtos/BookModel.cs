using Common.Dtos.Abstractions;

namespace Common.Dtos;

public sealed class BookModel : BookBase
{
    public Guid Id { get; set; }
    public string Image { get; set; } = string.Empty;
    public bool Bookmark { get; set; }
}