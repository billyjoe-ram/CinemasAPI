using System.Runtime.Serialization;

namespace UsuariosAPI.Exceptions
{
    public class RegisterException : UsuariosAPIException
    {
        public RegisterException() : base()
        {
        }

        public RegisterException(string? message) : base(message)
        {
        }

        public RegisterException(string? message, Exception? innerException) :
            base(message, innerException)
        {
        }

        protected RegisterException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
