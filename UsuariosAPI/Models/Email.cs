namespace UsuariosAPI.Models
{
    public class Email
    {
        public List<string> Destinatarios { get; private set;  } = new List<string>();
        public string Assunto { get; private set;  }
        public string CorpoEmail { get; private set; }

        public Email(List<string> destinatarios, string assunto, string corpoEmail)
        {
            Destinatarios = destinatarios;
            Assunto = assunto;
            CorpoEmail = corpoEmail;
        }

        public Email(string destinatario, string assunto, string corpoEmail)
        {
            Destinatarios.Add(destinatario);
            Assunto = assunto;
            CorpoEmail = corpoEmail;
        }
    }
}
