using AutoMapper;

using CinemasAPI.Data;
using CinemasAPI.Models;
using CinemasAPI.Data.Dtos.Filme;
using CinemasAPI.Exceptions;

namespace CinemasAPI.Services
{
    public class FilmesService
    {
        private AppDbContext _context;
        private IMapper _mapper;

        public FilmesService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public ReadFilmeDto AddFilme(CreateFilmeDto filmeDto)
        {
            Filme filme = _mapper.Map<Filme>(filmeDto);

            _context.Filmes.Add(filme);

            _context.SaveChanges();

            return _mapper.Map<ReadFilmeDto>(filme);
        }

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
