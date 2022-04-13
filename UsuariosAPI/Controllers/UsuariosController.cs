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

        [HttpPost]
        [Route("/[controller]/solicitar-reset-senha")]
        public IActionResult SolicitarResetSenha([FromBody] SolicitacaoResetSenhaRequest request)
        {
            string tokenResetSenha;

            try
            {
                tokenResetSenha = _usuariosService.SolicitarResetSenha(request);
            }
            catch (Exception e)
            {
                return Unauthorized(e.Message);
            }

            return Ok(new { Token = tokenResetSenha });
        }

        [HttpPost]
        [Route("/[controller]/reset-senha")]
        public IActionResult ResetarSenha([FromBody] ResetSenhaRequest request)
        {
            try
            {
                _usuariosService.ResetarSenha(request);
            }
            catch (Exception e)
            {
                return Unauthorized(e.Message);
            }

            return Ok(new { Mensagem = "Senha redefinida com sucesso" });
        }
    }
}
