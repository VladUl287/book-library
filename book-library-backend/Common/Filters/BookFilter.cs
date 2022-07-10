using Common.Filters.Abstractions;

namespace Common.Filters;

public class BookFilter : PageFilter
{
    public Guid? AuthorId { get; set; }
    public Guid[] Genres { get; set; } = Array.Empty<Guid>();
    public int? Rating { get; set; }
    public int? BeginYear { get; set; }
    public int? EndYear { get; set; }
}