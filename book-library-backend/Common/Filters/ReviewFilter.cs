using Common.Filters.Abstractions;

namespace Common.Filters
{
    public class ReviewFilter : PageFilter
    {
        public bool DateDesc { get; set; }
        public bool ViewsSort { get; set; }
    }
}