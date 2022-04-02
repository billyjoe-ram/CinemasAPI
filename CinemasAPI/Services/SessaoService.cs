using AutoMapper;

using CinemasAPI.Data;
using CinemasAPI.Models;
using CinemasAPI.Exceptions;
using CinemasAPI.Data.Dtos.Sessao;

namespace CinemasAPI.Services
{
    public class SessaoService
    {
        private AppDbContext _context;
        private IMapper _mapper;

        public SessaoService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public ReadSessaoDto AddSessao(CreateSessaoDto sessaoDto)
        {
            Sessao sessao = _mapper.Map<Sessao>(sessaoDto);

            _context.Sessoes.Add(sessao);
            _context.SaveChanges();

            return _mapper.Map<ReadSessaoDto>(sessao);
        }

        public IEnumerable<ReadSessaoDto> FetchSessoes()
        {
            var sessoes = _context.Sessoes.Select(s => _mapper.Map<ReadSessaoDto>(s));

            return sessoes;
        }

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
