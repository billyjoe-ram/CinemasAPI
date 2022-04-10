using UsuariosAPI.Services;

using Microsoft.AspNetCore.Mvc;

namespace UsuariosAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LogoutController : ControllerBase
    {
        private LogoutService _logoutService;

        public LogoutController(LogoutService logoutService)
        {
            _logoutService = logoutService;
        }

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
