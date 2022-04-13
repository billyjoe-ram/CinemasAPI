using System.Web;
using AutoMapper;

using UsuariosAPI.Models;
using UsuariosAPI.Exceptions;
using UsuariosAPI.Data.Dtos;
using UsuariosAPI.Data.Request;

using Microsoft.AspNetCore.Identity;

namespace UsuariosAPI.Services
{
    public class UsuariosService
    {
        private IMapper _mapper;
        private UserManager<IdentityUser<int>> _userManager;
        private EmailService _emailService;
        public UsuariosService(
            IMapper mapper,
            UserManager<IdentityUser<int>> userManager,
            EmailService emailService
        )
        {
            _mapper = mapper;
            _userManager = userManager;
            _emailService = emailService;
        }

        public string CadastrarUsuario(CreateUsuarioDto usuarioDto)
        {
            IdentityUser<int> usuarioIdentity = CriarUsuario(usuarioDto);

            string codigoConfirmacaoEmail = GerarCodigoConfirmacaoEmail(usuarioIdentity);
            var encodedCodigoConfirmacao = HttpUtility.UrlEncode(codigoConfirmacaoEmail);

            EnviarEmailConfirmacao(usuarioIdentity, encodedCodigoConfirmacao);

            return codigoConfirmacaoEmail;
        }

        private IdentityUser<int> CriarUsuario(CreateUsuarioDto usuarioDto)
        {
            Usuario usuario = _mapper.Map<Usuario>(usuarioDto);
            IdentityUser<int> usuarioIdentity = _mapper.Map<IdentityUser<int>>(usuario);

            Task<IdentityResult> resultadoIdentity = _userManager
                .CreateAsync(usuarioIdentity, usuarioDto.Senha);

            if (!resultadoIdentity.Result.Succeeded)
            {
                List<string> erros = resultadoIdentity.Result.Errors.Select(e => e.Description).ToList();
                string mensagemDeErro = String.Join("\n", erros);
                throw new RegisterException($"Erro ao cadastrar usuário:\n{mensagemDeErro}");
            }

            return usuarioIdentity;
        }

        private string GerarCodigoConfirmacaoEmail(IdentityUser<int> usuarioIdentity)
        {
            var taskCodigo = _userManager
                .GenerateEmailConfirmationTokenAsync(usuarioIdentity);

            string codigoConfirmacao = taskCodigo.Result;

            if (taskCodigo.IsCompleted && !taskCodigo.IsCompletedSuccessfully)
            {
                throw new RegisterException($"Erro ao gerar código de Confirmação Por E-mail");
            }

            return codigoConfirmacao;
        }

        private void EnviarEmailConfirmacao(IdentityUser<int> usuarioIdentity, string tokenAtivacao)
        {
            string destinatario = usuarioIdentity.Email;
            string assunto = "Link para ativação de conta";

            string corpoEmail = $"Olá {usuarioIdentity.UserName}!\n\n";
            corpoEmail += "Aqui seu link para ativação de conta no CinemasAPI.\n";
            corpoEmail += $"https://localhost:7099/usuarios/ativar?UsuarioId={usuarioIdentity.Id}&CodigoAtivacao ={tokenAtivacao}";

            _emailService.EnviarEmail(destinatario, assunto, corpoEmail);
        }

        public void AtivarUsuario(AtivaContaRequest ativaContaRequest)
        {
            var identityUser = _userManager
                .Users
                .First(u => u.Id == ativaContaRequest.UsuarioId);

            var identityResult = _userManager
                .ConfirmEmailAsync(identityUser, ativaContaRequest.CodigoAtivacao);

            if (!identityResult.Result.Succeeded)
            {
                throw new ActiveAccountByEmailException();
            }
        }

        public string SolicitarResetSenha(SolicitacaoResetSenhaRequest resetSenhaRequest)
        {
            IdentityUser<int> identityUser = GetUsuarioIdentityPorEmail(resetSenhaRequest.Email);

            Task<string> passwordResetTokenTask = _userManager.GeneratePasswordResetTokenAsync(identityUser);

            if (!passwordResetTokenTask.IsCompletedSuccessfully)
            {
                string mensagemExcecao = "Ocorreu um erro ao solicitar redefinição de senha";

                if (passwordResetTokenTask.Exception != null)
                {
                    mensagemExcecao = passwordResetTokenTask.Exception.Message;
                }
                throw new Exception(mensagemExcecao);
            }

            string passwordResetToken = passwordResetTokenTask.Result;

            return passwordResetToken;
        }

        public void ResetarSenha(ResetSenhaRequest request)
        {
            IdentityUser<int> identityUser = GetUsuarioIdentityPorEmail(request.Email);

            var identityResult = _userManager.ResetPasswordAsync(identityUser, request.ResetSenhaToken, request.ConfirmaSenha).Result;

            if (!identityResult.Succeeded)
            {
                throw new Exception($"Não foi possível redefinir a senha");
            }
        }

        private IdentityUser<int> GetUsuarioIdentityPorEmail(string email)
        {
            var identityUser = _userManager
                .Users
                .FirstOrDefault(u => u.NormalizedEmail == email.ToUpper());

            if (identityUser == null)
            {
                throw new NotFoundException("Usuario não encontrado");
            }

            return identityUser;
        }

    }
}
