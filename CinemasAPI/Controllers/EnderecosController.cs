using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using CinemasAPI.Data;
using CinemasAPI.Data.Dtos.Endereco;
using CinemasAPI.Models;

namespace CinemasAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EnderecosController : ControllerBase
    {
        private AppDbContext _context;
        private IMapper _mapper;

        public EnderecosController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        public IActionResult AddEndereco([FromBody] CreateEnderecoDto enderecoDto)
        {
            Endereco endereco = _mapper.Map<Endereco>(enderecoDto);

            _context.Enderecos.Add(endereco);
            _context.SaveChanges();

            return CreatedAtAction(
                nameof(FetchEndereco),
                new { IdEndereco = endereco.Id },
                endereco
            );
        }

        [HttpGet]
        public ActionResult<IEnumerable<Endereco>> FetchEnderecos()
        {
            var enderecos = _context.Enderecos.Select(e => _mapper.Map<ReadEnderecoDto>(e));

            return Ok(enderecos);
        }

        [HttpGet("{idEndereco}")]
        public ActionResult<Endereco> FetchEndereco(string idEndereco)
        {
            var endereco = FetchEnderecosPorId(idEndereco);

            if (endereco == null)
            {
                return NotFound();
            }

            ReadEnderecoDto enderecoDto = _mapper.Map<ReadEnderecoDto>(endereco);

            return Ok(enderecoDto);
        }

        [HttpPut("{idEndereco}")]
        public IActionResult UpdateEndereco(string idEndereco, [FromBody] UpdateEnderecoDto enderecoNovoDto)
        {
            var endereco = FetchEnderecosPorId(idEndereco);

            if (endereco == null)
            {
                return NotFound();
            }

            _mapper.Map(enderecoNovoDto, endereco);

            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{idEndereco}")]
        public IActionResult DeleteEndereco(string idEndereco)
        {
            var endereco = FetchEnderecosPorId(idEndereco);

            if (endereco == null)
            {
                return NotFound();
            }

            _context.Remove(endereco);
            _context.SaveChanges();

            return NoContent();
        }

        private Endereco FetchEnderecosPorId(string idEndereco)
        {
            var enderecoQuery = from e in _context.Enderecos
                              where e.Id.ToString() == idEndereco
                              select e;

            return enderecoQuery.FirstOrDefault();
        }
    }
}
