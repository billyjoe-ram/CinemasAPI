using AutoMapper;

using CinemasAPI.Data;
using CinemasAPI.Models;
using CinemasAPI.Data.Dtos.Gerente;

using Microsoft.AspNetCore.Mvc;

namespace CinemasAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GerentesController : ControllerBase
    {
        private AppDbContext _context;
        private IMapper _mapper;

        public GerentesController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        public IActionResult AddGerente([FromBody] CreateGerenteDto gerenteDto)
        {
            Gerente gerente = _mapper.Map<Gerente>(gerenteDto);

            _context.Gerentes.Add(gerente);
            _context.SaveChanges();

            return CreatedAtAction(
                nameof(FetchGerente),
                new { IdGerente = gerente.Id },
                gerente
            );
        }

        [HttpGet]
        public ActionResult<IEnumerable<Gerente>> Fefe()
        {
            var gerentes = _context.Gerentes.Select(g => _mapper.Map<ReadGerenteDto>(g));

            return Ok(gerentes);
        }

        [HttpGet("{idGerente}")]
        public ActionResult<Gerente> FetchGerente(string idGerente)
        {
            var gerente = FetchGerentePorId(idGerente);

            if (gerente == null)
            {
                return NotFound();
            }

            ReadGerenteDto gerenteDto = _mapper.Map<ReadGerenteDto>(gerente);

            return Ok(gerenteDto);
        }

        [HttpPut("{idGerente}")]
        public IActionResult UpdateGerente(string idGerente, [FromBody] UpdateGerenteDto gerenteNovoDto)
        {
            var gerente = FetchGerentePorId(idGerente);

            if (gerente == null)
            {
                return NotFound();
            }

            _mapper.Map(gerenteNovoDto, gerente);

            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{idGerente}")]
        public IActionResult DeleteGerente(string idGerente)
        {
            var gerente = FetchGerentePorId(idGerente);

            if (gerente == null)
            {
                return NotFound();
            }

            _context.Remove(gerente);
            _context.SaveChanges();

            return NoContent();
        }

        private Gerente FetchGerentePorId(string idGerente)
        {
            var gerenteQuery = from g in _context.Gerentes
                              where g.Id.ToString() == idGerente
                              select g;

            return gerenteQuery.FirstOrDefault();
        }
    }
}
