namespace Domain.Dtos;

public class CollectionCreate
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public BookView[] Books { get; set; } = Array.Empty<BookView>();
}