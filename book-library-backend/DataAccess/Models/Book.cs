using DataAccess.Abstractions;

namespace DataAccess.Models
{
    public class Book : EntityWithId<Guid>
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;
        public int PagesCount { get; set; }
        public ICollection<Author> Authors { get; set; } = Array.Empty<Author>();
        public ICollection<Review> Reviews { get; set; } = Array.Empty<Review>();
    }
}