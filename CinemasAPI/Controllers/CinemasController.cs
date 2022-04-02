using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using CinemasAPI.Data.Dtos.Cinema;
using CinemasAPI.Models;
using CinemasAPI.Services;
using CinemasAPI.Exceptions;

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
        public ActionResult<ReadCinemaDto> FetchCinema(string idCinema)
        {
            var cinema = _cinemasService.FetchCinema(idCinema);

            if (cinema == null)
            {
                return NotFound();
            }

            return Ok(cinema);
        }

        [HttpPut("{idCinema}")]
        public IActionResult UpdateCinema(string idCinema, [FromBody] UpdateCinemaDto cinemaNovoDto)
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
        public IActionResult DeleteCinema(string idCinema)
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
