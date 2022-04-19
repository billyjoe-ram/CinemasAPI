using AutoMapper;

using CinemasAPI.Data;
using CinemasAPI.Models;
using CinemasAPI.Exceptions;
using CinemasAPI.Data.Dtos.Gerente;

namespace CinemasAPI.Services
{
    /// <summary>
    /// 
    /// </summary>
    public class GerentesService
    {
        private AppDbContext _context;
        private IMapper _mapper;

        /// <summary>
        ///     Inicia uma nova instância da classe <see cref="GerentesService"/>
        /// </summary>
        /// <param name="context"></param>
        /// <param name="mapper"></param>
        public GerentesService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        ///     Adiciona um Gerente aos registros.
        /// </summary>
        /// <param name="gerenteDto">Representa o DTO para criação de um Gerente.</param>
        /// <returns>DTO para leitura de Gerente, com o Gerente criado.</returns>
        public ReadGerenteDto AddGerente(CreateGerenteDto gerenteDto)
        {
            Gerente gerente = _mapper.Map<Gerente>(gerenteDto);

            _context.Gerentes.Add(gerente);
            _context.SaveChanges();

            return _mapper.Map<ReadGerenteDto>(gerente);
        }

        /// <summary>
        ///     Busca todos os Gerentes no banco de dados.
        /// </summary>
        /// <returns>Uma coleçao de todos os Gerentes, formatado para leitura com DTO.</returns>
        public IEnumerable<ReadGerenteDto> FetchGerentes()
        {
            var gerentes = _context.Gerentes.Select(g => _mapper.Map<ReadGerenteDto>(g));

            return gerentes;
        }

        /// <summary>
        ///     Busca um Gerente no banco de dados com o id passado.
        /// </summary>
        /// <param name="idGerente">O id respectivo do Gerente no banco de dados.</param>
        /// <returns>O Gerente formatado para leitura com DTO.</returns>
        /// <exception cref="NotFoundException">
        ///     Lançada quando o id passado não corresponde a nenhum Gerente.
        /// </exception>
        public ReadGerenteDto FetchGerente(int idGerente)
        {
            Gerente gerente = FetchGerentePorId(idGerente);

            if (gerente == null)
            {
                throw new NotFoundException();
            }

            ReadGerenteDto gerenteDto = _mapper.Map<ReadGerenteDto>(gerente);

            return gerenteDto;
        }

        /// <summary>
        ///     Atualiza um Gerente no banco de dados com o id passado.
        /// </summary>
        /// <param name="idGerente">O id respectivo do Gerente a ser atualizado.</param>
        /// <param name="gerenteNovoDto">O novo objeto, com os dados a serem alterados.</param>
        /// <exception cref="NotFoundException">
        ///     Lançada quando o id passado não corresponde a nenhum Gerente.
        /// </exception>
        public void UpdateGerente(int idGerente, UpdateGerenteDto gerenteNovoDto)
        {
            Gerente gerente = FetchGerentePorId(idGerente);

            if (gerente == null)
            {
                throw new NotFoundException();
            }

            _mapper.Map(gerenteNovoDto, gerente);

            _context.SaveChanges();
        }

        /// <summary>
        ///     Exclue um registro de Gerente no banco de dados.
        /// </summary>
        /// <param name="idGerente">O id do Gerente a ser excluído.</param>
        /// <exception cref="NotFoundException">
        ///     Lançada quando o id passado não corresponde a nenhum Gerente.
        /// </exception>
        public void DeleteGerente(int idGerente)
        {
            Gerente gerente = FetchGerentePorId(idGerente);

            if (gerente == null)
            {
                throw new NotFoundException();
            }

            _context.Remove(gerente);
            _context.SaveChanges();
        }

        private Gerente FetchGerentePorId(int idGerente)
        {
            var gerenteQuery = from g in _context.Gerentes
                               where g.Id == idGerente
                               select g;

            return gerenteQuery.FirstOrDefault();
        }
    }
}
