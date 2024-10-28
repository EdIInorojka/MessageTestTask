namespace MessageTestTaskServer.ExceptionHandlers
{
    public class CustomException : Exception
    {
        public CustomException(string message) : base(message) { }
    }
}
