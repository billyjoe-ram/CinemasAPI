using UsuariosAPI.Exceptions;
using UsuariosAPI.Data.Request;

using Microsoft.AspNetCore.Identity;
using UsuariosAPI.Models;

namespace UsuariosAPI.Services
{
    public class LoginService
    {
        private SignInManager<IdentityUser<int>> _signInManager;
        private TokenService _tokenService;

        public LoginService(SignInManager<IdentityUser<int>> signInManager, TokenService tokenService)
        {
            _signInManager = signInManager;
            _tokenService = tokenService;
        }

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

            var token = _tokenService.CreateToken(identityUser);

            return token.Value;
        }
    }
}
