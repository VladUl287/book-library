namespace Common.Dtos;

public class CreateCollection
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public BookModel[] Books { get; set; } = Array.Empty<BookModel>();
}