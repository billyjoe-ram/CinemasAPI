using AutoMapper;

using CinemasAPI.Data;
using CinemasAPI.Models;
using CinemasAPI.Data.Dtos.Filme;

using Microsoft.AspNetCore.Mvc;

namespace CinemasAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FilmesController : ControllerBase
    {
        private AppDbContext _context;
        private IMapper _mapper;

        public FilmesController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        public ActionResult AddFilme([FromBody] CreateFilmeDto filmeDto)
        {
            Filme filme = _mapper.Map<Filme>(filmeDto);

            _context.Filmes.Add(filme);

            _context.SaveChanges();

            return CreatedAtAction(
                nameof(FetchFilme),
                new { IdFilme = filme.Id },
                filme
            );
        }

        [HttpGet]
        public ActionResult<IEnumerable<ReadFilmeDto>> FetchFilmes([FromQuery] int classificacaoEtaria = 18)
        {
            var filmes = _context.Filmes
                .Where(f => f.ClassificacaoEtaria <= classificacaoEtaria)
                .Select(f => _mapper.Map<ReadFilmeDto>(f));

            if (!filmes.Any())
            {
                return NotFound();
            }

            return Ok(filmes);
        }

        [HttpGet("{idFilme}")]
        public ActionResult<ReadFilmeDto> FetchFilme(int idFilme)
        {
            var filme = FetchFilmePorId(idFilme);

            if (filme == null)
            {
                return NotFound();
            }

            ReadFilmeDto filmeDto = _mapper.Map<ReadFilmeDto>(filme);

            return Ok(filmeDto);
        }

        [HttpPut("{idFilme}")]
        public IActionResult UpdateFilme(int idFilme, [FromBody] UpdateFilmeDto filmeNovoDto)
        {
            var filme = FetchFilmePorId(idFilme);

            if (filme == null) {
                return NotFound();
            }

            _mapper.Map(filmeNovoDto, filme);

            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{idFilme}")]
        public IActionResult DeleteFilme(int idFilme)
        {
            var filme = FetchFilmePorId(idFilme);

            if (filme == null)
            {
                return NotFound();
            }
            _context.Remove(filme);
            _context.SaveChanges();

            return NoContent();
        }

        private Filme FetchFilmePorId(int idFilme)
        {
            var filmeQuery = from f in _context.Filmes
                        where f.Id == idFilme
                        select f;

            return filmeQuery.FirstOrDefault();
        }
    }
}
