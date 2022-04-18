using AutoMapper;

using CinemasAPI.Data;
using CinemasAPI.Models;
using CinemasAPI.Exceptions;
using CinemasAPI.Data.Dtos.Cinema;

namespace CinemasAPI.Services
{
    /// <summary>
    ///     Service para as operações e regras de negócio relacionadas aos cinemas
    /// </summary>
    public class CinemasService
    {
        private AppDbContext _context;
        private IMapper _mapper;

        /// <summary>
        ///     Inicia uma nova instância da classe <see cref="CinemasService"/>.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="mapper"></param>
        public CinemasService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        ///     Adiciona um Cinema ao banco de dados.
        /// </summary>
        /// <param name="cinemaDto">DTO para criação de Cinema.</param>
        /// <returns>DTO para leitura de Cinema, com o Cinema criado.</returns>
        public ReadCinemaDto AddCinema(CreateCinemaDto cinemaDto)
        {
            Cinema cinema = _mapper.Map<Cinema>(cinemaDto);

            _context.Cinemas.Add(cinema);
            _context.SaveChanges();

            return _mapper.Map<ReadCinemaDto>(cinema);
        }

        /// <summary>
        ///     Busca um cinema no banco de dados com o id passado.
        /// </summary>
        /// <param name="idCinema">O id respectivo do Cinema no banco de dados.</param>
        /// <returns>O Cinema encontrado, formatado para exibição com DTO.</returns>
        /// <exception cref="NotFoundException">
        ///     Lançada quando o id passado não corresponde a nenhum cinema.
        /// </exception>
        public ReadCinemaDto FetchCinema(int idCinema)
        {
            var cinema = FetchCinemasPorId(idCinema);

            if (cinema == null)
            {
                throw new NotFoundException("Cinema não encontrado");
            }

            ReadCinemaDto cinemaDto = _mapper.Map<ReadCinemaDto>(cinema);

            return cinemaDto;
        }

        /// <summary>
        ///     Atualiza um cinema no banco de dados com o id passado.
        /// </summary>
        /// <param name="idCinema">O id respectivo do Cinema no banco de dados.</param>
        /// <param name="cinemaNovoDto">O novo objeto, com os dados a serem alterados.</param>
        /// <exception cref="NotFoundException">
        ///     Lançada quando o id passado não corresponde a nenhum cinema.
        /// </exception>
        public void UpdateCinema(int idCinema, UpdateCinemaDto cinemaNovoDto)
        {
            var cinema = FetchCinemasPorId(idCinema);

            if (cinema == null)
            {
                throw new NotFoundException("Cinema não encontrado");
            }

            _mapper.Map(cinemaNovoDto, cinema);

            _context.SaveChanges();
        }

        /// <summary>
        ///     Exclue um registro de cinema no banco de dados.
        /// </summary>
        /// <param name="idCinema">O id respectivo do Cinema no banco de dados.</param>
        /// <exception cref="NotFoundException">
        ///     Lançada quando o id passado não corresponde a nenhum cinema.
        /// </exception>
        public void DeleteCinema(int idCinema)
        {
            var cinema = FetchCinemasPorId(idCinema);

            if (cinema == null)
            {
                throw new NotFoundException("Cinema não encontrado");
            }

            _context.Remove(cinema);
            _context.SaveChanges();
        }

        private Cinema FetchCinemasPorId(int idCinema)
        {
            var cinemaQuery = from c in _context.Cinemas
                              where c.Id == idCinema
                              select c;

            return cinemaQuery.FirstOrDefault();
        }
    }
}
