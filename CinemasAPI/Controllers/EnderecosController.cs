using CinemasAPI.Services;
using CinemasAPI.Exceptions;
using CinemasAPI.Data.Dtos.Endereco;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace CinemasAPI.Controllers
{

    /// <summary>
    ///     Controller para as operações relacionadas a endereços
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    internal class EnderecosController : ControllerBase
    {
        private EnderecosService _enderecosService;

        /// <summary>
        ///     Inicia uma nova instância da classe <see cref="EnderecosController"/> .
        /// </summary>
        /// <param name="enderecosService"></param>
        public EnderecosController(EnderecosService enderecosService)
        {
            _enderecosService = enderecosService;
        }

        /// <summary>
        ///     Adiciona um Endereço aos registros.
        /// </summary>
        /// <param name="enderecoDto">Representa o DTO para criação de Endereco.</param>
        /// <returns>Resultado de ação realizada.</returns>
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

        /// <summary>
        ///     Busca todo os Endereços no banco de dados.
        /// </summary>
        /// <returns>Resultado da ação, com os Endereços em JSON.</returns>
        [HttpGet]
        [Authorize(Roles = "admin, user")]
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

        /// <summary>
        ///     Busca um Endereço no banco de dados com o id passado.
        /// </summary>
        /// <param name="idEndereco">O id respectivo do Endereço no banco de dados.</param>
        /// <returns>Resultado da ação, com o Endereço em JSON.</returns>
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

        /// <summary>
        ///     Atualiza um Endereço no banco de dados com o id passado.
        /// </summary>
        /// <param name="idEndereco">O id respectivo do Endereço a ser atualizado.</param>
        /// <param name="enderecoNovoDto">O novo objeto, com os dados a serem alterados.</param>
        /// <returns>Resultado da ação realizada.</returns>
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

        /// <summary>
        ///     Exclue um registro de Endereço no banco de dados.
        /// </summary>
        /// <param name="idEndereco">O id do Endereço a ser excluído.</param>
        /// <returns>Resultado da ação realizada.</returns>
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
