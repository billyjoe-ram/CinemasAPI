using CinemasAPI.Services;
using CinemasAPI.Exceptions;
using CinemasAPI.Data.Dtos.Filme;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace CinemasAPI.Controllers
{
    /// <summary>
    ///     Controller para as operações relacionadas a endereços
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class FilmesController : ControllerBase
    {
        private FilmesService _filmesService;

        /// <summary>
        ///     Inicia uma nova instância da classe <see cref="EnderecosController"/>.
        /// </summary>
        /// <param name="filmesService"></param>
        public FilmesController(FilmesService filmesService)
        {
            _filmesService = filmesService;
        }

        /// <summary>
        ///     Adiciona um Filme aos registros.
        /// </summary>
        /// <param name="filmeDto">Representa o DTO para criação de Filme.</param>
        /// <returns>Resultado de ação realizada.</returns>
        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult AddFilme([FromBody] CreateFilmeDto filmeDto)
        {
            ReadFilmeDto filme = _filmesService.AddFilme(filmeDto);

            return CreatedAtAction(
                nameof(FetchFilme),
                new { IdFilme = filme.Id },
                filme
            );
        }

        /// <summary>
        ///     Busca todos os Filmes no banco de dados.
        /// </summary>
        /// <remarks>É possível filtrar os registros por meio da classificação etária.</remarks>
        /// <param name="classificacaoEtaria"> Idade para filtrar classificação etária.ss</param>
        /// <returns>Resultado da ação, com os Filmes em JSON.</returns>
        [HttpGet]
        [Authorize(Roles = "admin, user")]
        public ActionResult<IEnumerable<ReadFilmeDto>> FetchFilmes([FromQuery] int classificacaoEtaria = 18)
        {
            IEnumerable<ReadFilmeDto> filmes;

            try
            {
                filmes = _filmesService.FetchFilmes(classificacaoEtaria);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }

            return Ok(filmes);
        }

        /// <summary>
        ///     Busca um Filme no banco de dados com o id passado.
        /// </summary>
        /// <param name="idFilme">O id respectivo do Filme no banco de dados.</param>
        /// <returns>Resultado da ação, com o Filme em JSON.</returns>
        [HttpGet("{idFilme}")]
        [Authorize(Roles = "admin, user")]
        public ActionResult<ReadFilmeDto> FetchFilme(int idFilme)
        {
            ReadFilmeDto filme;

            try
            {
                filme = _filmesService.FetchFilme(idFilme);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }

            return Ok(filme);
        }

        /// <summary>
        ///     Atualiza um Filme no banco de dados com o id passado.
        /// </summary>
        /// <param name="idFilme">O id respectivo do filme a ser atualizado.</param>
        /// <param name="filmeNovoDto">O novo objeto, com os dados a serem alterados.</param>
        /// <returns>Resultado da ação realizada.</returns>
        [HttpPut("{idFilme}")]
        [Authorize(Roles = "admin")]
        public IActionResult UpdateFilme(int idFilme, [FromBody] UpdateFilmeDto filmeNovoDto)
        {
            try
            {
                _filmesService.UpdateFilme(idFilme, filmeNovoDto);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }

            return NoContent();
        }

        /// <summary>
        ///     Exclue um registro de Filme no banco de dados
        /// </summary>
        /// <param name="idFilme">O id do filme a ser excluído.</param>
        /// <returns>Resultado a ação reallizada.</returns>
        [HttpDelete("{idFilme}")]
        [Authorize(Roles = "admin")]
        public IActionResult DeleteFilme(int idFilme)
        {
            try
            {
                _filmesService.DeleteFilme(idFilme);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
            return NoContent();
        }

    }
}
