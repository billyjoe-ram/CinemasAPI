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
            Filme filme = new Filme(filmeDto.Titulo, filmeDto.Diretor, filmeDto.DuracaoEmMinutos);

            filme.ClassificacaoEtaria = filmeDto.ClassificacaoEtaria;

            _context.Filmes.Add(filme);

            _context.SaveChanges();

            return CreatedAtAction(
                nameof(FetchFilme),
                new { Id = filme.Id },
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
        public ActionResult<ReadFilmeDto> FetchFilme(string idFilme)
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
        public IActionResult UpdateFilme(string idFilme, [FromBody] UpdateFilmeDto filmeNovoDto)
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
        public IActionResult DeleteFilme(string idFilme)
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

        private Filme FetchFilmePorId(string idFilme)
        {
            var filmeQuery = from f in _context.Filmes
                        where f.Id.ToString() == idFilme
                        select f;

            return filmeQuery.FirstOrDefault();
        }
    }
}
