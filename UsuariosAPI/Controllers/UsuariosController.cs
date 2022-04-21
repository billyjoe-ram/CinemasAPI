using UsuariosAPI.Services;
using UsuariosAPI.Data.Dtos;
using UsuariosAPI.Data.Request;

using Microsoft.AspNetCore.Mvc;

namespace UsuariosAPI.Controllers
{
    /// <summary>
    ///     Controller para as ações relacionadas ao usuário.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    internal class UsuariosController : ControllerBase
    {
        private UsuariosService _usuariosService;

        /// <summary>
        ///     Inicia uma nova instância da classe <see cref="UsuariosController"/>
        /// </summary>
        /// <param name="usuariosService"></param>
        public UsuariosController(UsuariosService usuariosService)
        {
            _usuariosService = usuariosService;
        }

        /// <summary>
        ///     Cadastra um usuário no sistema.
        /// </summary>
        /// <param name="usuarioDto">Classe de Request para cadastrar o usuário.</param>
        /// <returns>Resultado da ação realizada.</returns>
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

        /// <summary>
        ///     Ativa o usuário.
        /// </summary>
        /// <remarks>
        ///     Utiliza o token ao cadastrar para ativação gerado.
        /// </remarks>
        /// <param name="ativaContaRequest">Classe de Request para ativar a conta.</param>
        /// <returns>Resultado da ação realizada.</returns>
        [HttpGet]
        [Route("/[controller]/ativar")]
        public IActionResult AtivarUsuario([FromQuery] AtivaContaRequest ativaContaRequest)
        {
            try
            {
                _usuariosService.AtivarUsuario(ativaContaRequest);
            }
            catch (Exception e)
            {
                return BadRequest(new { Mensagem = e.Message });
            }
            return Ok(new { Mensagem = "Conta ativada com sucesso"});
        }

        /// <summary>
        ///     Solicita a redefinição da senha.
        /// </summary>
        /// <remarks>
        ///     Para o usuário enviado, será gerado um Token, necessário para redefinição.
        /// </remarks>
        /// <param name="request">Classe de Request para solicitar reset de senha.</param>
        /// <returns>Resultado da ação realizada.</returns>
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
                return Unauthorized(new { Mensagem = e.Message });
            }

            return Ok(new { Token = tokenResetSenha });
        }

        /// <summary>
        ///     Redefine a senha do usuário.
        /// </summary>
        /// <param name="request">Classe de Request para redefir senha.</param>
        /// <returns>Resultado da ação realizada.</returns>
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
                return Unauthorized(new { Mensagem = e.Message });
            }

            return Ok(new { Mensagem = "Senha redefinida com sucesso" });
        }
    }
}
