namespace Domain.Filters.Abstractions;

public class PageFilter
{
    public int? Page { get; init; }
    public int? Size { get; init; }
}