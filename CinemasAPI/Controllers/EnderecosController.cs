using CinemasAPI.Services;
using CinemasAPI.Exceptions;
using CinemasAPI.Data.Dtos.Endereco;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace CinemasAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EnderecosController : ControllerBase
    {
        private EnderecosService _enderecosService;

        public EnderecosController(EnderecosService enderecosService)
        {
            _enderecosService = enderecosService;
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public IActionResult AddEndereco([FromBody] CreateEnderecoDto enderecoDto)
        {
            ReadEnderecoDto endereco = _enderecosService.AddEndereco(enderecoDto);

            return CreatedAtAction(
                nameof(FetchEndereco),
                new { IdEndereco = endereco.Id },
                endereco
            );
        }

        [HttpGet]
        public ActionResult<IEnumerable<ReadEnderecoDto>> FetchEnderecos()
        {
            IEnumerable<ReadEnderecoDto> enderecos;

            try
            {
                enderecos = _enderecosService.FetchEnderecos();
            }
            catch (NotFoundException)
            {
                return NotFound();
            }

            return Ok(enderecos);
        }

        [HttpGet("{idEndereco}")]
        public ActionResult<ReadEnderecoDto> FetchEndereco(int idEndereco)
        {
            ReadEnderecoDto endereco;

            try
            {
                endereco = _enderecosService.FetchEndereco(idEndereco);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
            
            return Ok(endereco);
        }

        [HttpPut("{idEndereco}")]
        [Authorize(Roles = "admin")]
        public IActionResult UpdateEndereco(int idEndereco, [FromBody] UpdateEnderecoDto enderecoNovoDto)
        {
            try
            {
                _enderecosService.UpdateEndereco(idEndereco, enderecoNovoDto);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{idEndereco}")]
        [Authorize(Roles = "admin")]
        public IActionResult DeleteEndereco(int idEndereco)
        {
            try
            {
                _enderecosService.DeleteEndereco(idEndereco);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
