﻿using CinemasAPI.Services;
using CinemasAPI.Exceptions;
using CinemasAPI.Data.Dtos.Sessao;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

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
        [Authorize(Roles = "admin")]
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
        [Authorize(Roles = "admin, user")]
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
        [Authorize(Roles = "admin, user")]
        public ActionResult<ReadSessaoDto> FetchSessao(int idSessao)
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
        [Authorize(Roles = "admin")]
        public IActionResult UpdateSessao(int idSessao, [FromBody] UpdateSessaoDto sessaoNovaDto)
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
        [Authorize(Roles = "admin")]
        public IActionResult DeleteSessao(int idSessao)
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
