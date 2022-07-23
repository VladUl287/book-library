using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos;

public class ReviewModel
{
    public Guid Id { get; set; }
    public string Text { get; set; } = string.Empty;
    [Range(1, 10)]
    public int Rating { get; set; }
    public Guid BookId { get; set; }
}