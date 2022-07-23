using Domain.Dtos.Abstractions;

namespace Domain.Dtos;

public sealed class BookView : BookBase
{
    public Guid Id { get; set; }
    public string Image { get; set; } = string.Empty;
}