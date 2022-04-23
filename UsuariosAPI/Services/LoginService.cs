using UsuariosAPI.Exceptions;
using UsuariosAPI.Data.Request;

using Microsoft.AspNetCore.Identity;

namespace UsuariosAPI.Services
{
    /// <summary>
    ///     Service para as operações e regras de negócio relacionado ao login.
    /// </summary>
    public class LoginService
    {
        private SignInManager<IdentityUser<int>> _signInManager;
        private TokenService _tokenService;

        /// <summary>
        ///     Inicia uma nova instância da classe <see cref="LoginService"/>.
        /// </summary>
        /// <param name="signInManager"></param>
        /// <param name="tokenService"></param>
        public LoginService(SignInManager<IdentityUser<int>> signInManager, TokenService tokenService)
        {
            _signInManager = signInManager;
            _tokenService = tokenService;
        }

        /// <summary>
        ///     Loga um usuário no sistema.
        /// </summary>
        /// <param name="request">Classe de Request para efetuar o login.</param>
        /// <returns>O token de autenticação após realizar o login.</returns>
        /// <exception cref="SignInException">
        ///     Lançada quando não é possível realizar o login.
        /// </exception>
        public string LogarUsuario(LoginRequest request)
        {           
            var identityUser = _signInManager
                .UserManager
                .Users
                .Where(u => u.NormalizedUserName == request.Username.ToUpper())
                .First();

            if (!identityUser.EmailConfirmed)
            {
                throw new SignInException("Confirme seu e-mail para logar");
            }

            var resultadoIdentity = _signInManager.PasswordSignInAsync(request.Username, request.Senha, true, false);

            if (!resultadoIdentity.Result.Succeeded)
            {
                throw new SignInException("Não foi possível realizar o login.");
            }

            var rolesUsuario = _signInManager.UserManager.GetRolesAsync(identityUser).Result.First();

            var token = _tokenService.CreateUserToken(
                identityUser,
                rolesUsuario
            );

            return token.Value;
        }
    }
}
