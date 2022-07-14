namespace Common.Dtos;

public class CollectionModel : CreateCollection
{
    public Guid Id { get; set; }
    public int Views { get; set; }
    public int Likes { get; set; }
}