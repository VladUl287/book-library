using DataAccess.Abstractions;

namespace DataAccess.Models
{
    public class Author : EntityWithId<Guid>
    {
        public string Name { get; set; } = string.Empty;
        public ICollection<Book> Books { get; set; } = Array.Empty<Book>();
    }
}