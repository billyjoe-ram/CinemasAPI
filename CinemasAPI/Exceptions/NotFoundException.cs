namespace CinemasAPI.Exceptions
{
    public class NotFoundException : CinemasAPIException
    {
        public NotFoundException() : base()
        {
        }

        public NotFoundException(string? message) : base(message)
        {
        }

        public NotFoundException(string? message, Exception? innerException) :
            base(message, innerException)
        {
        }
    }
}
