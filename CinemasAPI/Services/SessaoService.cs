using AutoMapper;

using CinemasAPI.Data;
using CinemasAPI.Models;
using CinemasAPI.Exceptions;
using CinemasAPI.Data.Dtos.Sessao;

namespace CinemasAPI.Services
{
    /// <summary>
    ///     Service para as operações e regras de negócio relacionadas a Sessões.
    /// </summary>
    internal class SessaoService
    {
        private AppDbContext _context;
        private IMapper _mapper;

        /// <summary>
        ///     Inicia uma nova instância da classe <see cref="SessaoService"/>
        /// </summary>
        /// <param name="context"></param>
        /// <param name="mapper"></param>
        public SessaoService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        ///     Adiciona uma Sessão aos registros.
        /// </summary>
        /// <param name="sessaoDto">Representa o DTO para a criação de uma Sessão.</param>
        /// <returns>DTO para leitura de Sessão, com a Sessão criado.</returns>
        public ReadSessaoDto AddSessao(CreateSessaoDto sessaoDto)
        {
            Sessao sessao = _mapper.Map<Sessao>(sessaoDto);

            _context.Sessoes.Add(sessao);
            _context.SaveChanges();

            return _mapper.Map<ReadSessaoDto>(sessao);
        }

        /// <summary>
        ///     Busca todas as Sessões no banco de dados.
        /// </summary>
        /// <returns>Uma coleçao de todas as Sessões, formatadas para leitura com DTO.</returns>
        public IEnumerable<ReadSessaoDto> FetchSessoes()
        {
            var sessoes = _context.Sessoes.Select(s => _mapper.Map<ReadSessaoDto>(s));

            return sessoes;
        }

        /// <summary>
        ///     Busca uma Sessão no banco de dados com o id passado.
        /// </summary>
        /// <param name="idSessao">O id respectivo da Sessão no banco de dados.</param>
        /// <returns>A Sessão formatada para leitura com DTO.</returns>
        /// <exception cref="NotFoundException">
        ///     Lançada quando o id passado não corresponde a nenhuma Sessão.
        /// </exception>
        public ReadSessaoDto FetchSessao(int idSessao)
        {
            Sessao sessao = FetchSessaoPorId(idSessao);

            if (sessao == null)
            {
                throw new NotFoundException();
            }

            ReadSessaoDto sessaoDto = _mapper.Map<ReadSessaoDto>(sessao);

            return sessaoDto;
        }

        /// <summary>
        ///     Atualiza uma Sessão no banco de dados com o id passado.
        /// </summary>
        /// <param name="idSessao">O id respectivo da Sessão a ser atualizada.</param>
        /// <param name="sessaoNovaDto">O novo objeto, com os dados a serem alterados.</param>
        /// <exception cref="NotFoundException">
        ///     Lançada quando o id passado não corresponde a nenhuma Sessão.
        /// </exception>
        public void UpdateSessao(int idSessao, UpdateSessaoDto sessaoNovaDto)
        {
            var sessao = FetchSessaoPorId(idSessao);

            if (sessao == null)
            {
                throw new NotFoundException();
            }

            _mapper.Map(sessaoNovaDto, sessao);

            _context.SaveChanges();
        }

        /// <summary>
        ///     Exclue um registro de Sessão no banco de dados.
        /// </summary>
        /// <param name="idSessao">O id da Sessão a ser excluída.</param>
        /// <exception cref="NotFoundException">
        ///     Lançada quando o id passado não corresponde a nenhuma Sessão.
        /// </exception>
        public void DeleteSessao(int idSessao)
        {
            Sessao sessao = FetchSessaoPorId(idSessao);

            if (sessao == null)
            {
                throw new NotFoundException();
            }

            _context.Remove(sessao);
            _context.SaveChanges();
        }

        private Sessao FetchSessaoPorId(int idSessao)
        {
            var sessaoQuery = from s in _context.Sessoes
                              where s.Id == idSessao
                              select s;

            return sessaoQuery.FirstOrDefault();
        }   
    }
}
