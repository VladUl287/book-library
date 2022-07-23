namespace Domain.Dtos;

public class ErrorResponse
{
    public List<ErrorModel> Errors { get; set; } = new();
}