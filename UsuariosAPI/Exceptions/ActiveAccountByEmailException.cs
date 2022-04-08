using System.Runtime.Serialization;

namespace UsuariosAPI.Exceptions
{
    public class ActiveAccountByEmailException : UsuariosAPIException
    {
        public ActiveAccountByEmailException()
        {
        }

        public ActiveAccountByEmailException(string? message) : base(message)
        {
        }

        public ActiveAccountByEmailException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected ActiveAccountByEmailException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}