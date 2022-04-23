using System.Web;
using AutoMapper;

using UsuariosAPI.Models;
using UsuariosAPI.Exceptions;
using UsuariosAPI.Data.Dtos;
using UsuariosAPI.Data.Request;

using Microsoft.AspNetCore.Identity;

namespace UsuariosAPI.Services
{
    /// <summary>
    ///     Service para as operações e regras de negócio relacionado ao usuário.
    /// </summary>
    public class UsuariosService
    {
        private IMapper _mapper;
        private UserManager<IdentityUser<int>> _userManager;
        private EmailService _emailService;
        private RoleManager<IdentityRole<int>> _roleManager;

        /// <summary>
        ///     Inicia uma nova instância da classe <see cref="UsuariosService"/>
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="userManager"></param>
        /// <param name="emailService"></param>
        /// <param name="roleManager"></param>
        public UsuariosService(
            IMapper mapper,
            UserManager<IdentityUser<int>> userManager,
            EmailService emailService,
            RoleManager<IdentityRole<int>> roleManager
        )
        {
            _mapper = mapper;
            _userManager = userManager;
            _emailService = emailService;
            _roleManager = roleManager;
        }

        /// <summary>
        ///     Cadastra um usuário no sistema.
        /// </summary>
        /// <param name="usuarioDto">DTO para cadastrar um usuário.</param>
        /// <returns>Código de confirmação por e-mail.</returns>
        public async Task<string> CadastrarUsuario(CreateUsuarioDto usuarioDto)
        {
            IdentityUser<int> usuarioIdentity = await CriarUsuario(usuarioDto);

            await AdicionarRoles(usuarioIdentity);

            string codigoConfirmacaoEmail = await GerarCodigoConfirmacaoEmail(usuarioIdentity);
            var encodedCodigoConfirmacao = HttpUtility.UrlEncode(codigoConfirmacaoEmail);

            EnviarEmailConfirmacao(usuarioIdentity, encodedCodigoConfirmacao);

            return codigoConfirmacaoEmail;
        }

        private async Task<IdentityUser<int>> CriarUsuario(CreateUsuarioDto usuarioDto)
        {
            Usuario usuario = _mapper.Map<Usuario>(usuarioDto);
            IdentityUser<int> usuarioIdentity = _mapper.Map<IdentityUser<int>>(usuario);

            IdentityResult resultadoIdentity = await _userManager
                .CreateAsync(usuarioIdentity, usuarioDto.Senha);

            if (!resultadoIdentity.Succeeded)
            {
                List<string> erros = resultadoIdentity.Errors.Select(e => e.Description).ToList();
                string mensagemDeErro = String.Join("\n", erros);
                throw new RegisterException($"Erro ao cadastrar usuário:\n{mensagemDeErro}");
            }

            return usuarioIdentity;
        }
        private async Task<bool> AdicionarRoles(IdentityUser<int> usuarioIdentity)
        {
            IdentityResult taskUsuarioRole = await _userManager.AddToRoleAsync(usuarioIdentity, "user");

            if (!taskUsuarioRole.Succeeded)
            {
                throw new RegisterException($"Erro ao adicionar roles");
            }

            return taskUsuarioRole.Succeeded;
        }
        private async Task<string> GerarCodigoConfirmacaoEmail(IdentityUser<int> usuarioIdentity)
        {
            string codigoConfirmacao;

            try
            {
                codigoConfirmacao = await _userManager
                    .GenerateEmailConfirmationTokenAsync(usuarioIdentity);
            }
            catch (Exception)
            {
                throw new RegisterException($"Erro ao gerar código de Confirmação Por E-mail"); ;
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


        /// <summary>
        ///     Ativa o usuário.
        /// </summary>
        /// <remarks>
        ///     Utiliza o token ao cadastrar para ativação gerado.
        /// </remarks>
        /// <param name="ativaContaRequest">Classe de Request para ativar a conta.</param>
        /// <exception cref="ActiveAccountByEmailException"></exception>
        public async Task<bool> AtivarUsuario(AtivaContaRequest ativaContaRequest)
        {
            var identityUser = _userManager
                .Users
                .First(u => u.Id == ativaContaRequest.UsuarioId);

            var identityResult = await _userManager
                .ConfirmEmailAsync(identityUser, ativaContaRequest.CodigoAtivacao);

            if (!identityResult.Succeeded)
            {
                List<string> erros = identityResult.Errors.Select(e => e.Description).ToList();
                string mensagemDeErro = String.Join("\n", erros);
                throw new ActiveAccountByEmailException($"Erro ao ativar conta: {mensagemDeErro}");
            }

            return identityResult.Succeeded;
        }


        /// <summary>
        ///     Solicita a redefinição da senha.
        /// </summary>
        /// <remarks>
        ///     Para o usuário enviado, será gerado um Token, necessário para redefinição.
        /// </remarks>
        /// <param name="resetSenhaRequest">Classe de Request para solicitar reset de senha.</param>
        /// <returns>Token de autenticação para reset de senha.</returns>
        /// <exception cref="Exception">
        ///     Lançada caso ocorra um erro nas tasks internas.
        /// </exception>
        public async Task<string> SolicitarResetSenha(SolicitacaoResetSenhaRequest resetSenhaRequest)
        {
            string passwordResetToken;
            IdentityUser<int> identityUser = GetUsuarioIdentityPorEmail(resetSenhaRequest.Email);

            try
            {
                passwordResetToken = await _userManager
                    .GeneratePasswordResetTokenAsync(identityUser);
            }
            catch (Exception)
            {
                throw new Exception("Ocorreu um erro ao solicitar redefinição de senha");
            }

            return passwordResetToken;
        }

        /// <summary>
        ///     Redefine a senha do usuário.
        /// </summary>
        /// <param name="request">Classe de Request para redefir senha.</param>
        /// <exception cref="Exception">
        ///     Lançada caso ocorra um erro nas tasks internas.
        /// </exception>
        public async Task<bool> ResetarSenha(ResetSenhaRequest request)
        {
            IdentityUser<int> identityUser = GetUsuarioIdentityPorEmail(request.Email);

            var identityResult = await _userManager.ResetPasswordAsync(identityUser, request.ResetSenhaToken, request.ConfirmaSenha);

            if (!identityResult.Succeeded)
            {
                throw new Exception($"Não foi possível redefinir a senha");
            }

            return identityResult.Succeeded;
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
