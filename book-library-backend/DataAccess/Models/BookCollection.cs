using DataAccess.Abstractions;

namespace DataAccess.Models;

public class BookCollection : EntityWithId<Guid>
{
    public Guid BookId { get; set; }
    public Book Book { get; set; }
    public Guid CollectionId { get; set; }
    public Collection Collection { get; set; } 
}