using Domain.Filters.Abstractions;

namespace Domain.Filters;

public class CollectionFilter : PageFilter
{
    public bool ViewsSort { get; set; }
}