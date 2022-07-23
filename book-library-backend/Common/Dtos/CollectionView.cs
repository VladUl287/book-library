namespace Domain.Dtos;

public class CollectionView : CollectionCreate
{
    public Guid Id { get; set; }
    public int Views { get; set; }
    public int Likes { get; set; }
}