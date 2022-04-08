using System.Runtime.Serialization;

namespace UsuariosAPI.Exceptions
{
    public class UsuariosAPIException : Exception
    {
        public UsuariosAPIException() : base()
        {
        }

        public UsuariosAPIException(string? message) : base(message)
        {
        }

        public UsuariosAPIException(string? message, Exception? innerException) :
            base(message, innerException)
        {
        }

        protected UsuariosAPIException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
