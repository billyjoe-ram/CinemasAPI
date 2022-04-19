using AutoMapper;

using CinemasAPI.Data;
using CinemasAPI.Models;
using CinemasAPI.Exceptions;
using CinemasAPI.Data.Dtos.Filme;

namespace CinemasAPI.Services
{
    /// <summary>
    ///     Service para as operações e regras de negócio relacionadas aos Filmes.
    /// </summary>
    public class FilmesService
    {
        private AppDbContext _context;
        private IMapper _mapper;

        /// <summary>
        ///     Inicia uma nova instância da classe <see cref="FilmesService"/>
        /// </summary>
        /// <param name="context"></param>
        /// <param name="mapper"></param>
        public FilmesService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        ///     Adiciona um Filme aos registros.
        /// </summary>
        /// <param name="filmeDto">DTO para criação de Filme.</param>
        /// <returns>DTO para leitura de Filme, com o Filme criado.</returns>
        public ReadFilmeDto AddFilme(CreateFilmeDto filmeDto)
        {
            Filme filme = _mapper.Map<Filme>(filmeDto);

            _context.Filmes.Add(filme);

            _context.SaveChanges();

            return _mapper.Map<ReadFilmeDto>(filme);
        }

        /// <summary>
        ///     Busca um Filme no banco de dados com o id passado.
        /// </summary>
        /// <param name="idFilme">O id respectivo do Filme no banco de dados.</param>
        /// <returns>O Filme formatado para leitura com DTO</returns>
        /// <exception cref="NotFoundException">
        ///     Lançada quando o id passado não corresponde a nenhum Filme.
        /// </exception>
        public ReadFilmeDto FetchFilme(int idFilme)
        {
            var filme = FetchFilmePorId(idFilme);

            if (filme == null)
            {
                throw new NotFoundException("Filme não encontrado");
            }

            ReadFilmeDto filmeDto = _mapper.Map<ReadFilmeDto>(filme);

            return filmeDto;
        }

        /// <summary>
        ///     Busca todos os Filmes no banco de dados.
        /// </summary>
        /// <param name="classificacaoEtaria"> Idade para filtrar classificação etária.ss</param>
        /// <returns>Uma coleçao de todos os Filmes, formatado para leitura com DTO.</returns>
        /// <exception cref="NotFoundException">
        ///     Lançada quando não há nenhum registro de Filmes.
        /// </exception>
        public IEnumerable<ReadFilmeDto> FetchFilmes(int classificacaoEtaria = 18)
        {
            var filmes = _context.Filmes
                .Where(f => f.ClassificacaoEtaria <= classificacaoEtaria)
                .Select(f => _mapper.Map<ReadFilmeDto>(f));

            if (!filmes.Any())
            {
                throw new NotFoundException("Filmes não encontrados");
            }

            return filmes;
        }

        /// <summary>
        ///     Atualiza um Filme no banco de dados com o id passado.
        /// </summary>
        /// <param name="idFilme">O id respectivo do filme a ser atualizado.</param>
        /// <param name="filmeNovoDto">O novo objeto, com os dados a serem alterados.</param>
        /// <exception cref="NotFoundException">
        ///     Lançada quando o id passado não corresponde a nenhum Filme.
        /// </exception>
        public void UpdateFilme(int idFilme, UpdateFilmeDto filmeNovoDto)
        {
            var filme = FetchFilmePorId(idFilme);

            if (filme == null)
            {
                throw new NotFoundException("Filme não encontrado");
            }

            _mapper.Map(filmeNovoDto, filme);

            _context.SaveChanges();
        }

        /// <summary>
        ///     Exclue um registro de Filme no banco de dados.
        /// </summary>
        /// <param name="idFilme">O id respectivo do Filme a ser excluído.</param>
        /// <exception cref="NotFoundException">
        ///     Lançada quando o id passado não corresponde a nenhum Filme.
        /// </exception>
        public void DeleteFilme(int idFilme)
        {
            var filme = FetchFilmePorId(idFilme);

            if (filme == null)
            {
                throw new NotFoundException("Filme não encontrado");
            }
            _context.Remove(filme);
            _context.SaveChanges();
        }

        private Filme FetchFilmePorId(int idFilme)
        {
            var filmeQuery = from f in _context.Filmes
                             where f.Id == idFilme
                             select f;

            return filmeQuery.FirstOrDefault();
        }
    }
}
