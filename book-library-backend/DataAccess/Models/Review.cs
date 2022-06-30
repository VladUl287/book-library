using DataAccess.Abstractions;

namespace DataAccess.Models
{
    public class Review : EntityWithId<Guid>
    {
        public string Text { get; set; } = string.Empty;
        public int Rating { get; set; }
        public Guid BookId { get; set; }
        public Book Book { get; set; }
    }
}