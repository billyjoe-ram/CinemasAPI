using CinemasAPI.Services;
using CinemasAPI.Exceptions;
using CinemasAPI.Data.Dtos.Gerente;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace CinemasAPI.Controllers
{
    /// <summary>
    ///     Controller para as ações relacionadas a gerentes
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    internal class GerentesController : ControllerBase
    {
        private GerentesService _gerentesService;

        /// <summary>
        ///     Inicia uma nova instância da classe <see cref="GerentesService"/>
        /// </summary>
        /// <param name="gerenteService"></param>
        public GerentesController(GerentesService gerenteService)
        {
            _gerentesService = gerenteService;
        }

        /// <summary>
        ///     Adiciona um Gerente aos registros.
        /// </summary>
        /// <param name="gerenteDto">Representa o DTO para criação de um Gerente.</param>
        /// <returns>Resultado da ação realizada.</returns>
        [HttpPost]
        [Authorize(Roles = "admin")]
        public IActionResult AddGerente([FromBody] CreateGerenteDto gerenteDto)
        {
            ReadGerenteDto gerente = _gerentesService.AddGerente(gerenteDto);

            return CreatedAtAction(
                nameof(FetchGerente),
                new { IdGerente = gerente.Id },
                gerente
            );
        }

        /// <summary>
        ///     Busca todos os Gerentes no banco de dados.
        /// </summary>
        /// <returns>Resultado da ação, com os Gerentes em JSON.</returns>
        [HttpGet]
        [Authorize(Roles = "admin, user")]
        public ActionResult<IEnumerable<ReadGerenteDto>> FetchGerentes()
        {
            IEnumerable<ReadGerenteDto> gerentes;

            try
            {
                gerentes = _gerentesService.FetchGerentes();
            }
            catch (NotFoundException)
            {
                return NotFound();
            }

            return Ok(gerentes);
        }

        /// <summary>
        ///     Busca um Gerente no banco de dados com o id passado.
        /// </summary>
        /// <param name="idGerente">O id respectivo do Gerente no banco de dados.</param>
        /// <returns>Resultado da ação, com o Gerente em JSON.</returns>
        [HttpGet("{idGerente}")]
        [Authorize(Roles = "admin, user")]
        public ActionResult<ReadGerenteDto> FetchGerente(int idGerente)
        {
            ReadGerenteDto gerente;

            try
            {
                gerente = _gerentesService.FetchGerente(idGerente);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }

            return Ok(gerente);
        }

        /// <summary>
        ///     Atualiza um Gerente no banco de dados com o id passado.
        /// </summary>
        /// <param name="idGerente">O id respectivo do Gerente a ser atualizado.</param>
        /// <param name="gerenteNovoDto">O novo objeto, com os dados a serem alterados.</param>
        /// <returns>Resultado da ação realizada</returns>
        [HttpPut("{idGerente}")]
        [Authorize(Roles = "admin")]
        public IActionResult UpdateGerente(int idGerente, [FromBody] UpdateGerenteDto gerenteNovoDto)
        {
            try
            {
                _gerentesService.UpdateGerente(idGerente, gerenteNovoDto);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }

            return NoContent();
        }


        /// <summary>
        ///     Exclue um registro de Gerente no banco de dados.
        /// </summary>
        /// <param name="idGerente">O id do Gerente a ser excluído.</param>
        /// <returns>Resultado da ação realizada.</returns>
        [HttpDelete("{idGerente}")]
        [Authorize(Roles = "admin")]
        public IActionResult DeleteGerente(int idGerente)
        {
            try
            {
                _gerentesService.DeleteGerente(idGerente);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
