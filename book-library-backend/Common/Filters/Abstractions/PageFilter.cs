namespace Common.Filters.Abstractions;

public abstract class PageFilter
{
    public int? Page { get; init; }
    public int? Size { get; init; }
}