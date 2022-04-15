using CinemasAPI.Services;
using CinemasAPI.Exceptions;
using CinemasAPI.Data.Dtos.Cinema;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace CinemasAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CinemasController : ControllerBase
    {
        private CinemasService _cinemasService;

        public CinemasController(CinemasService cinemasService)
        {
            _cinemasService = cinemasService;
        }

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

        [HttpGet("{idCinema}")]
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
