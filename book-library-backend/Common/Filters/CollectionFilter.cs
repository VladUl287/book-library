using Common.Filters.Abstractions;

namespace Common.Filters;

public class CollectionFilter : PageFilter
{
    public bool ByPopular { get; set; }
}