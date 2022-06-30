namespace Common.Errors
{
    public class Error
    {
        public string ErrorDescription { get; private set; }

        public Error(string error)
        {
            ErrorDescription = error;
        }
    }
}