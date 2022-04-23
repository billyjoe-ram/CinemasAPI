namespace CinemasAPI.Exceptions
{
    public class CinemasAPIException : Exception
    {
        public CinemasAPIException() : base()
        {
        }

        public CinemasAPIException(string? message) : base(message)
        {
        }

        public CinemasAPIException(string? message, Exception? innerException) :
            base(message, innerException)
        {
        }
    }
}
