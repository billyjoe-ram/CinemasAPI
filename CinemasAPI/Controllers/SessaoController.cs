using CinemasAPI.Services;
using CinemasAPI.Exceptions;
using CinemasAPI.Data.Dtos.Sessao;

using Microsoft.AspNetCore.Mvc;

namespace CinemasAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SessaoController : ControllerBase
    {
        private SessaoService _sessaoService;

        public SessaoController(SessaoService sessaoService)
        {
            _sessaoService = sessaoService;
        }

        [HttpPost]
        public IActionResult AddSessao([FromBody] CreateSessaoDto sessaoDto)
        {
            ReadSessaoDto sessao = _sessaoService.AddSessao(sessaoDto);

            return CreatedAtAction(
                nameof(FetchSessao),
                new { IdSessao = sessao.Id },
                sessao
            );
        }

        [HttpGet]
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

        [HttpGet("{idSessao}")]
        public ActionResult<ReadSessaoDto> FetchSessao(string idSessao)
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

        [HttpPut("{idSessao}")]
        public IActionResult UpdateSessao(string idSessao, [FromBody] UpdateSessaoDto sessaoNovaDto)
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

        [HttpDelete("{idSessao}")]
        public IActionResult DeleteSessao(string idSessao)
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
