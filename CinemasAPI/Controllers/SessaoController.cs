using AutoMapper;

using CinemasAPI.Data;
using CinemasAPI.Models;
using CinemasAPI.Data.Dtos.Sessao;

using Microsoft.AspNetCore.Mvc;

namespace CinemasAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SessaoController : ControllerBase
    {
        private AppDbContext _context;
        private IMapper _mapper;

        public SessaoController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        public IActionResult AddSessao([FromBody] CreateSessaoDto sessaoDto)
        {
            Sessao sessao = _mapper.Map<Sessao>(sessaoDto);

            _context.Sessoes.Add(sessao);
            _context.SaveChanges();

            return CreatedAtAction(
                nameof(FetchSessao),
                new { IdSessao = sessao.Id },
                sessao
            );
        }

        [HttpGet]
        public ActionResult<IEnumerable<Sessao>> FetchSessoes()
        {
            var sessoes = _context.Sessoes.Select(s => _mapper.Map<ReadSessaoDto>(s));

            return Ok(sessoes);
        }

        [HttpGet("{idSessao}")]
        public ActionResult<Sessao> FetchSessao(string idSessao)
        {
            var sessao = FetchSessaoPorId(idSessao);

            if (sessao == null)
            {
                return NotFound();
            }

            ReadSessaoDto sessaoDto = _mapper.Map<ReadSessaoDto>(sessao);

            return Ok(sessaoDto);
        }

        [HttpPut("{idSessao}")]
        public IActionResult UpdateSessao(string idSessao, [FromBody] UpdateSessaoDto sessaoNovaDto)
        {
            var sessao = FetchSessaoPorId(idSessao);

            if (sessao == null)
            {
                return NotFound();
            }

            _mapper.Map(sessaoNovaDto, sessao);

            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{idSessao}")]
        public IActionResult DeleteSessao(string idSessao)
        {
            var sessao = FetchSessaoPorId(idSessao);

            if (sessao == null)
            {
                return NotFound();
            }

            _context.Remove(sessao);
            _context.SaveChanges();

            return NoContent();
        }

        private Sessao FetchSessaoPorId(string idSessao)
        {
            var sessaoQuery = from s in _context.Sessoes
                              where s.Id.ToString() == idSessao
                              select s;

            return sessaoQuery.FirstOrDefault();
        }
    }
}
