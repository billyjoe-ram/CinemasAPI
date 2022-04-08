using UsuariosAPI.Services;
using Microsoft.AspNetCore.Mvc;
using UsuariosAPI.Data.Request;

namespace UsuariosAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        private LoginService _loginService;

        public LoginController(LoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpPost]
        public IActionResult LogarUsuario(LoginRequest request)
        {
            string tokenLogin;

            try
            {
                tokenLogin = _loginService.LogarUsuario(request);
            }
            catch (Exception e)
            {
                return Unauthorized(new { Message = e.Message });
            }

            return Ok(new { Usuario = request.Username, Token = tokenLogin });
        }
    }
}
