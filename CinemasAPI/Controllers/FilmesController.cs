using CinemasAPI.Services;
using CinemasAPI.Exceptions;
using CinemasAPI.Data.Dtos.Filme;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace CinemasAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FilmesController : ControllerBase
    {
        private FilmesService _filmesService;

        public FilmesController(FilmesService filmesService)
        {
            _filmesService = filmesService;
        }

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
