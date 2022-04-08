using System.Runtime.Serialization;

namespace UsuariosAPI.Exceptions
{
    public class LogoutException : UsuariosAPIException
    {
        public LogoutException()
        {
        }

        public LogoutException(string? message) : base(message)
        {
        }

        public LogoutException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected LogoutException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}