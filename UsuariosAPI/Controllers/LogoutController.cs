using UsuariosAPI.Services;

using Microsoft.AspNetCore.Mvc;

namespace UsuariosAPI.Controllers
{
    /// <summary>
    ///     Controller para as ações relacionadas a logout
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class LogoutController : ControllerBase
    {
        private LogoutService _logoutService;

        /// <summary>
        ///     Inicia uma nova instância da classe <see cref="LogoutController"/>
        /// </summary>
        /// <param name="logoutService"></param>
        public LogoutController(LogoutService logoutService)
        {
            _logoutService = logoutService;
        }

        /// <summary>
        ///     Desconecta um usuário do sistema.
        /// </summary>
        /// <returns>Resultado da ação realizada.</returns>
        public IActionResult LogoutUsuario()
        {
            try
            {
                _logoutService.LogoutUsuario();
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
            return Ok();
        }
    }
}
