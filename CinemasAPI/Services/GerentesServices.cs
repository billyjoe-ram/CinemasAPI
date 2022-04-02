using AutoMapper;

using CinemasAPI.Data;
using CinemasAPI.Models;
using CinemasAPI.Exceptions;
using CinemasAPI.Data.Dtos.Gerente;

namespace CinemasAPI.Services
{
    public class GerentesService
    {
        private AppDbContext _context;
        private IMapper _mapper;

        public GerentesService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public ReadGerenteDto AddGerente(CreateGerenteDto gerenteDto)
        {
            Gerente gerente = _mapper.Map<Gerente>(gerenteDto);

            _context.Gerentes.Add(gerente);
            _context.SaveChanges();

            return _mapper.Map<ReadGerenteDto>(gerente);
        }

        public IEnumerable<ReadGerenteDto> FetchGerentes()
        {
            var gerentes = _context.Gerentes.Select(g => _mapper.Map<ReadGerenteDto>(g));

            return gerentes;
        }

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
