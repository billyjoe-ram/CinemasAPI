using System.Runtime.Serialization;

namespace UsuariosAPI.Exceptions
{
    public class SignInException : UsuariosAPIException
    {
        public SignInException()
        {
        }

        public SignInException(string? message) : base(message)
        {
        }

        public SignInException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected SignInException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}