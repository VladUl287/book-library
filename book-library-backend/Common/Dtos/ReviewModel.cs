namespace Common.Dtos;

public class ReviewModel
{
    public string Text { get; set; } = string.Empty;
    public int Rating { get; set; }
    public Guid BookId { get; set; }
}