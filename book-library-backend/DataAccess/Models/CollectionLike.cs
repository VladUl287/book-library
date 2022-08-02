namespace DataAccess.Models;

public class CollectionLike
{
    public Guid UserId { get; set; }
    public User User { get; set; }
    public Guid CollectionId { get; set; }
    public Collection Collection { get; set; }
}