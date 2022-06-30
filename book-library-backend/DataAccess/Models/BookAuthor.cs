using DataAccess.Abstractions;

namespace DataAccess.Models
{
    public class BookAuthor : EntityWithId<Guid>
    {
        public Guid BookId { get; set; }
        public Book Book { get; set; }
        public Guid AuthorId { get; set; }
        public Author Author { get; set; }
    }
}
