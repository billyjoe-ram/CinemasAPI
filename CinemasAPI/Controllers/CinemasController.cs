using CinemasAPI.Services;
using CinemasAPI.Exceptions;
using CinemasAPI.Data.Dtos.Cinema;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace CinemasAPI.Controllers
{
    /// <summary>
    ///     Controller para as operações relacionadas aos cinemas
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    internal class CinemasController : ControllerBase
    {
    private CinemasService _cinemasService;

        /// <summary>
        ///     Inicia uma nova instância da classe <see cref="CinemasController"/> .
        /// </summary>
        /// <param name="cinemasService"></param>
        public CinemasController(CinemasService cinemasService)
        {
            _cinemasService = cinemasService;
        }

        /// <summary>
        ///     Adiciona um Cinema aos registros.
        /// </summary>
        /// <param name="cinemaDto">Representa o DTO para criação do Cinema.</param>
        /// <returns>Resultado de ação realizada.</returns>
        [HttpPost]
        [Authorize(Roles = "admin")]
        public IActionResult AddCinema([FromBody] CreateCinemaDto cinemaDto)
        {
            ReadCinemaDto cinema = _cinemasService.AddCinema(cinemaDto);

            return CreatedAtAction(
                nameof(FetchCinema),
                new { IdCinema = cinema.Id },
                cinema
            );
        }

        /// <summary>
        ///     Busca um cinema no banco de dados com o id passado.
        /// </summary>
        /// <param name="idCinema">O id respectivo do Cinema no banco de dados.</param>
        /// <returns>Resultado da ação, com o Cinema em JSON.</returns>
        [HttpGet("{idCinema}")]
        [Authorize(Roles = "admin, user")]
        public ActionResult<ReadCinemaDto> FetchCinema(int idCinema)
        {
            ReadCinemaDto cinema;

            try
            {
                cinema = _cinemasService.FetchCinema(idCinema);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }

            return Ok(cinema);
        }

        /// <summary>
        ///     Atualiza um cinema no banco de dados com o id passado.
        /// </summary>
        /// <param name="idCinema">O id respectivo do Cinema no banco de dados.</param>
        /// <param name="cinemaNovoDto">O novo objeto, com os dados a serem alterados.</param>
        /// <returns>Resultado da ação realizada.</returns>
        [HttpPut("{idCinema}")]
        [Authorize(Roles = "admin")]
        public IActionResult UpdateCinema(int idCinema, [FromBody] UpdateCinemaDto cinemaNovoDto)
        {
            try
            {
                _cinemasService.UpdateCinema(idCinema, cinemaNovoDto);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }

            return NoContent();
        }

        /// <summary>
        ///     Exclue um registro de cinema no banco de dados.
        /// </summary>
        /// <param name="idCinema">O id respectivo do Cinema no banco de dados.</param>
        /// <returns>Resultado da ação realizada.</returns>
        [HttpDelete("{idCinema}")]
        [Authorize(Roles = "admin")]
        public IActionResult DeleteCinema(int idCinema)
        {
            try
            {
                _cinemasService.DeleteCinema(idCinema);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
