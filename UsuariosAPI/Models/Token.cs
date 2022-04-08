namespace UsuariosAPI.Models
{
    public class Token
    {
        public string Value { get; private set; }
        public Token(string value)
        {
            Value = value;
        }
    }
}
