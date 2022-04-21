using CinemasAPI.Services;
using CinemasAPI.Exceptions;
using CinemasAPI.Data.Dtos.Sessao;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace CinemasAPI.Controllers
{
    /// <summary>
    ///     Controller para as ações relacionadas a Sessões
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    internal class SessaoController : ControllerBase
    {
        private SessaoService _sessaoService;

        /// <summary>
        ///     Inicia uma nova instância da classe <see cref="SessaoController"/>
        /// </summary>
        /// <param name="sessaoService"></param>
        public SessaoController(SessaoService sessaoService)
        {
            _sessaoService = sessaoService;
        }

        /// <summary>
        ///     Adiciona uma Sessão aos registros.
        /// </summary>
        /// <param name="sessaoDto">Representa o DTO para a criação de uma Sessão.</param>
        /// <returns>Resultado da ação realizada.</returns>
        [HttpPost]
        [Authorize(Roles = "admin")]
        public IActionResult AddSessao([FromBody] CreateSessaoDto sessaoDto)
        {
            ReadSessaoDto sessao = _sessaoService.AddSessao(sessaoDto);

            return CreatedAtAction(
                nameof(FetchSessao),
                new { IdSessao = sessao.Id },
                sessao
            );
        }

        /// <summary>
        ///     Busca todas as Sessões no banco de dados.
        /// </summary>
        /// <returns>Resultado da ação, com as Sessões em JSON.</returns>
        [HttpGet]
        [Authorize(Roles = "admin, user")]
        public ActionResult<IEnumerable<ReadSessaoDto>> FetchSessoes()
        {
            IEnumerable<ReadSessaoDto> sessoes;

            try
            {
                sessoes = _sessaoService.FetchSessoes();
            }
            catch (NotFoundException)
            {
                return NotFound();
            }

            return Ok(sessoes);
        }

        /// <summary>
        ///     Busca uma Sessão no banco de dados com o id passado.
        /// </summary>
        /// <param name="idSessao">O id respectivo da Sessão no banco de dados.</param>
        /// <returns> Resultado da ação, com a Sessão em JSON.</returns>
        [HttpGet("{idSessao}")]
        [Authorize(Roles = "admin, user")]
        public ActionResult<ReadSessaoDto> FetchSessao(int idSessao)
        {
            ReadSessaoDto sessao;

            try
            {
                sessao = _sessaoService.FetchSessao(idSessao);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }

            return Ok(sessao);
        }

        /// <summary>
        ///     Atualiza uma Sessão no banco de dados com o id passado.
        /// </summary>
        /// <param name="idSessao">O id respectivo da Sessão a ser atualizada.</param>
        /// <param name="sessaoNovaDto">O novo objeto, com os dados a serem alterados.</param>
        /// <returns>Resultado da ação realizada.</returns>
        [HttpPut("{idSessao}")]
        [Authorize(Roles = "admin")]
        public IActionResult UpdateSessao(int idSessao, [FromBody] UpdateSessaoDto sessaoNovaDto)
        {
            try
            {
                _sessaoService.UpdateSessao(idSessao, sessaoNovaDto);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }

            return NoContent();
        }

        /// <summary>
        ///     Exclue um registro de Sessão no banco de dados.
        /// </summary>
        /// <param name="idSessao">O id da Sessão a ser excluída.</param>
        /// <returns>Resultado da ação realizada.</returns>
        [HttpDelete("{idSessao}")]
        [Authorize(Roles = "admin")]
        public IActionResult DeleteSessao(int idSessao)
        {
            try
            {
                _sessaoService.DeleteSessao(idSessao);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
