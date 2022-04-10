using UsuariosAPI.Exceptions;

using Microsoft.AspNetCore.Identity;

namespace UsuariosAPI.Services
{
    public class LogoutService
    {
        private SignInManager<IdentityUser<int>> _signInManager;
        private TokenService _tokenService;

        public LogoutService(SignInManager<IdentityUser<int>> signInManager, TokenService tokenService)
        {
            _tokenService = tokenService;
            _signInManager = signInManager;
        }

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
