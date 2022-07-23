using Domain.Filters.Abstractions;

namespace Domain.Filters;

public class ReviewFilter : PageFilter
{
    public bool ViewsSort { get; set; }
}