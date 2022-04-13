namespace UsuariosAPI.Exceptions
{
    public class NotFoundException : UsuariosAPIException
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
