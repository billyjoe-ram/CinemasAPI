using UsuariosAPI.Services;
using UsuariosAPI.Data.Dtos;
using UsuariosAPI.Data.Request;

using Microsoft.AspNetCore.Mvc;

namespace UsuariosAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuariosController : ControllerBase
    {
        private UsuariosService _usuariosService;
        public UsuariosController(UsuariosService usuariosService)
        {
            _usuariosService = usuariosService;
        }

        [HttpPost]
        public IActionResult CadastrarUsuario(CreateUsuarioDto usuarioDto)
        {
            string codigoAtivacao;
            try
            {
                codigoAtivacao = _usuariosService.CadastrarUsuario(usuarioDto);
            }
            catch (Exception e)
            {
                return BadRequest(new { Mensagem = e.Message});
            }

            return Ok(new { CodigoAtivacao = codigoAtivacao });
        }

        [HttpGet]
        [Route("/[controller]/ativar")]
        public IActionResult AtivarUsuario([FromQuery] AtivaContaRequest ativaContaRequest)
        {
            try
            {
                _usuariosService.AtivarUsuario(ativaContaRequest);
            }
            catch (Exception)
            {
                return BadRequest(new { Mensagem = "Não foi possível ativar a conta" });
            }
            return Ok(new { Mensagem = "Conta ativada com sucesso"});
        }
    }
}
