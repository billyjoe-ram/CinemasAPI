using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using CinemasAPI.Data;
using CinemasAPI.Data.Dtos.Cinema;
using CinemasAPI.Models;

namespace CinemasAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CinemasController : ControllerBase
    {
        private AppDbContext _context;
        private IMapper _mapper;

        public CinemasController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        public IActionResult AddCinema([FromBody] CreateCinemaDto cinemaDto)
        {
            Cinema cinema = _mapper.Map<Cinema>(cinemaDto);

            _context.Cinemas.Add(cinema);
            _context.SaveChanges();

            return CreatedAtAction(
                nameof(FetchCinema),
                new { IdCinema = cinema.Id },
                cinema
            );
        }

        [HttpGet("{idCinema}")]
        public ActionResult<Cinema> FetchCinema(string idCinema)
        {
            var cinema = FetchCinemasPorId(idCinema);

            if (cinema == null)
            {
                return NotFound();
            }

            ReadCinemaDto cinemaDto = _mapper.Map<ReadCinemaDto>(cinema);

            return Ok(cinemaDto);
        }

        [HttpPut("{idCinema}")]
        public IActionResult UpdateCinema(string idCinema, [FromBody] UpdateCinemaDto cinemaNovoDto)
        {
            var cinema = FetchCinemasPorId(idCinema);

            if (cinema == null)
            {
                return NotFound();
            }

            _mapper.Map(cinemaNovoDto, cinema);

            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{idCinema}")]
        public IActionResult DeleteCinema(string idCinema)
        {
            var cinema = FetchCinemasPorId(idCinema);

            if (cinema == null)
            {
                return NotFound();
            }

            _context.Remove(cinema);
            _context.SaveChanges();

            return NoContent();
        }

        private Cinema FetchCinemasPorId(string idCinema)
        {
            var cinemaQuery = from c in _context.Cinemas
                              where c.Id.ToString() == idCinema
                              select c;

            return cinemaQuery.FirstOrDefault();
        }
    }
}
