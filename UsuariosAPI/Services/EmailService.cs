using MimeKit;
using MailKit.Net.Smtp;

using UsuariosAPI.Models;

namespace UsuariosAPI.Services
{
    public class EmailService
    {
        private IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void EnviarEmail(List<string> destinatarios, string assunto, string corpoEmail)
        {
            var email = new Email(destinatarios, assunto, corpoEmail);

            EnviarEmailMime(email);
        }

        public void EnviarEmail(string destinatario, string assunto, string corpoEmail)
        {
            var email = new Email(destinatario, assunto, corpoEmail);

            EnviarEmailMime(email);
        }

        private void EnviarEmailMime(Email email)
        {
            var mimeMessage = ToMimeMessage(email);

            using (var client = new SmtpClient())
            {
                try
                {
                    client.Connect(
                        _configuration.GetValue<string>("EmailSettings:SmtpServer"),
                        _configuration.GetValue<int>("EmailSettings:Port"),
                        true
                    );
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    client.Authenticate(
                        _configuration.GetValue<string>("EmailSettings:From"),
                        _configuration.GetValue<string>("EmailSettings:Password")
                    );
                    client.Send(mimeMessage);
                }
                catch (Exception)
                {
                    throw;
                } finally
                {
                    client.Disconnect(true);
                    client.Dispose();
                }
            }
        }

        public MimeMessage ToMimeMessage(Email email)
        {
            MimeMessage mimeMessage = new MimeMessage();

            var fromAddress = new MailboxAddress(
                "CinemasAPI",
                _configuration.GetValue<string>("EmailSettings:From")
            );
            mimeMessage.From.Add(fromAddress);

            List<MailboxAddress> destinatarios = new List<MailboxAddress>();
            destinatarios.AddRange(email.Destinatarios.Select(d => new MailboxAddress("", d)));
            mimeMessage.To.AddRange(destinatarios);

            mimeMessage.Subject = email.Assunto;

            mimeMessage.Body = new TextPart(MimeKit.Text.TextFormat.Text)
            {
                Text = email.CorpoEmail
            };

            return mimeMessage;
        }
    }
}
