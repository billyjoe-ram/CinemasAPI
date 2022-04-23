using UsuariosAPI.Services;
using UsuariosAPI.Data.Request;

using Microsoft.AspNetCore.Mvc;

namespace UsuariosAPI.Controllers
{
    /// <summary>
    ///     Controller para as ações relacionadas a login.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        private LoginService _loginService;

        /// <summary>
        ///     Inicia uma nova instância da classe <see cref="LoginController"/>.
        /// </summary>
        /// <param name="loginService"></param>
        public LoginController(LoginService loginService)
        {
            _loginService = loginService;
        }

        /// <summary>
        ///     Loga um usuário no sistema.
        /// </summary>
        /// <param name="request">Classe de Request para efetuar o login.</param>
        /// <returns>Resultado da ação realizada.</returns>
        [HttpPost]
        public async Task<IActionResult> LogarUsuario(LoginRequest request)
        {
            string tokenLogin;

            try
            {
                tokenLogin = await _loginService.LogarUsuario(request);
            }
            catch (Exception e)
            {
                return Unauthorized(new { Message = e.Message });
            }

            return Ok(new { Usuario = request.Username, Token = tokenLogin });
        }
    }
}
