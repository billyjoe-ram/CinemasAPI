using MimeKit;
using MailKit.Net.Smtp;

using UsuariosAPI.Models;

namespace UsuariosAPI.Services
{
    /// <summary>
    ///     Service para as operações e regras de negócio relacionadas aos
    ///     e-mails da UsuariosAPI
    /// </summary>
    internal class EmailService
    {
        private IConfiguration _configuration;

        /// <summary>
        ///     Inicia uma nova instância da classe <see cref="EmailService"/>
        /// </summary>
        /// <param name="configuration"></param>
        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        ///     Envia um e-mail para a lista de destinatários informados.
        /// </summary>
        /// <remarks>
        ///     Utiliza o remetente padrão da API para enviar o e-mail.
        /// </remarks>
        /// <param name="destinatarios">Lista de e-mails aos quais a mensagem será enviada.</param>
        /// <param name="assunto">Assunto da mensagem de e-mail.</param>
        /// <param name="corpoEmail">Corpo do texto do e-mail.</param>
        public void EnviarEmail(List<string> destinatarios, string assunto, string corpoEmail)
        {
            var email = new Email(destinatarios, assunto, corpoEmail);

            EnviarEmailMime(email);
        }

        /// <summary>
        ///     Envia um e-mail para o destinatário informados.
        /// </summary>
        /// <remarks>
        ///     Utiliza o remetente padrão da API para enviar o e-mail.
        /// </remarks>
        /// <param name="destinatario">Endereço de e-mail ao qual a mensagem será enviada.</param>
        /// <param name="assunto">Assunto da mensagem de e-mail.</param>
        /// <param name="corpoEmail">Corpo do texto do e-mail.</param>
        public void EnviarEmail(string destinatario, string assunto, string corpoEmail)
        {
            var email = new Email(destinatario, assunto, corpoEmail);

            EnviarEmailMime(email);
        }

        /// <summary>
        ///     Envia um e-mail com o auxílio do MimeKit
        /// </summary>
        /// <param name="email">Uma classe <see cref="Email"/></param>
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

        /// <summary>
        ///     Converte uma classe <see cref="Email"/> para uma
        ///     <see cref="MimeMessage"/> do MimeKit.
        /// </summary>
        /// <param name="email">A classe de e-mail.</param>
        /// <returns>
        ///     A classe <see cref="Email"/> convertida para <see cref="MimeMessage"/>.
        /// </returns>
        private MimeMessage ToMimeMessage(Email email)
        {
            MimeMessage mimeMessage = new MimeMessage();

            var fromAddress = new MailboxAddress(
                "CinemasAPI",
                _configuration.GetValue<string>("EmailSettings:From")
            );
            mimeMessage.From.Add(fromAddress);

            List<MailboxAddress> destinatarios = new List<MailboxAddress>();
            destinatarios.AddRange(email
                .Destinatarios
                .Select(d => new MailboxAddress("", d)));
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
