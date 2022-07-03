namespace Common.Dtos;

public class CollectionModel
{
    public string Description { get; set; } = string.Empty;
    public int Views { get; set; }
    public int Likes { get; set; }
    public ICollection<BookModel> Books { get; set; } = Array.Empty<BookModel>();
}