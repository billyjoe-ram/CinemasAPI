using CinemasAPI.Services;
using CinemasAPI.Exceptions;
using CinemasAPI.Data.Dtos.Gerente;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace CinemasAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GerentesController : ControllerBase
    {
        private GerentesService _gerentesService;

        public GerentesController(GerentesService gerenteService)
        {
            _gerentesService = gerenteService;
        }

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

        [HttpGet]
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

        [HttpGet("{idGerente}")]
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
