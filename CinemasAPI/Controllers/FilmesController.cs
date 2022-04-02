using Microsoft.AspNetCore.Mvc;

using CinemasAPI.Services;
using CinemasAPI.Data.Dtos.Filme;
using CinemasAPI.Exceptions;

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
        public ActionResult<IEnumerable<ReadFilmeDto>> FetchFilmes([FromQuery] int classificacaoEtaria = 18)
        {
            var filmes = _filmesService.FetchFilme(classificacaoEtaria);

            if (filmes == null)
            {
                return NotFound();
            }

            return Ok(filmes);
        }

        [HttpGet("{idFilme}")]
        public ActionResult<ReadFilmeDto> FetchFilme(int idFilme)
        {
            var filme = _filmesService.FetchFilme(idFilme);

            if (filme == null)
            {
                return NotFound();
            }

            return Ok(filme);
        }

        [HttpPut("{idFilme}")]
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
