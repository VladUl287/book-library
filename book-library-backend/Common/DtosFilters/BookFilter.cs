using Domain.Filters.Abstractions;

namespace Domain.Filters;

public class BookFilter : PageFilter
{
    public string Name { get; set; } = string.Empty;
    public Guid[] Genres { get; set; } = Array.Empty<Guid>();
    public int? Rating { get; set; }
    public int? BeginYear { get; set; }
    public int? EndYear { get; set; }
}