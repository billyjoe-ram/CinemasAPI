using UsuariosAPI.Exceptions;

using Microsoft.AspNetCore.Identity;

namespace UsuariosAPI.Services
{
    /// <summary>
    ///     Service para as operações e regras de negócio relacionado ao logout.
    /// </summary>
    internal class LogoutService
    {
        private SignInManager<IdentityUser<int>> _signInManager;
        private TokenService _tokenService;

        /// <summary>
        ///     Inicia uma nova instância da classe <see cref="LogoutService"/>
        /// </summary>
        /// <param name="signInManager"></param>
        /// <param name="tokenService"></param>
        public LogoutService(SignInManager<IdentityUser<int>> signInManager, TokenService tokenService)
        {
            _tokenService = tokenService;
            _signInManager = signInManager;
        }

        /// <summary>
        ///     Desconecta um usuário do sistema.
        /// </summary>
        /// <exception cref="LogoutException">
        ///     Lançada quando não é possível realizar o logout.
        /// </exception>
        public void LogoutUsuario()
        {
            var resultadoIdentity = _signInManager.SignOutAsync();

            if (!resultadoIdentity.IsCompletedSuccessfully)
            {
                string mensagemErro = "Não foi possível realizar o logout";

                if (resultadoIdentity.Exception != null)
                {
                    mensagemErro += $":\n{resultadoIdentity.Exception.Message}.";
                }
                throw new LogoutException(mensagemErro);
            }
        }
    }
}
