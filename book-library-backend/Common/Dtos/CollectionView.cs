namespace Domain.Dtos;

public class CollectionView
{
    public Guid Id { get; set; }
    public string Image { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public int Views { get; set; }
    public int Likes { get; set; }
    public BookView[] Books { get; set; } = Array.Empty<BookView>();
}