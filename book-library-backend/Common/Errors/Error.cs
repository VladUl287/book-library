namespace Domain.Errors;

public class Error
{
    public string ErrorDescription { get; }

    public Error(string error)
    {
        ErrorDescription = error;
    }
}