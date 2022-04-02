using AutoMapper;

using CinemasAPI.Data;
using CinemasAPI.Models;
using CinemasAPI.Exceptions;
using CinemasAPI.Data.Dtos.Endereco;

namespace CinemasAPI.Services
{
    public class EnderecosService
    {
        private AppDbContext _context;
        private IMapper _mapper;

        public EnderecosService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public ReadEnderecoDto AddEndereco(CreateEnderecoDto enderecoDto)
        {
            Endereco endereco = _mapper.Map<Endereco>(enderecoDto);

            _context.Enderecos.Add(endereco);
            _context.SaveChanges();

            return _mapper.Map<ReadEnderecoDto>(endereco);
        }

        public IEnumerable<ReadEnderecoDto> FetchEnderecos()
        {
            var enderecos = _context.Enderecos.Select(e => _mapper.Map<ReadEnderecoDto>(e));

            if (!enderecos.Any())
            {
                throw new NotFoundException();
            }

            return enderecos;
        }

        public ReadEnderecoDto FetchEndereco(int idEndereco)
        {
            var endereco = FetchEnderecosPorId(idEndereco);

            if (endereco == null)
            {
                throw new NotFoundException();
            }

            return _mapper.Map<ReadEnderecoDto>(endereco);
        }

        public void UpdateEndereco(int idEndereco, UpdateEnderecoDto enderecoNovoDto)
        {
            Endereco endereco = FetchEnderecosPorId(idEndereco);

            if (endereco == null)
            {
                throw new NotFoundException();
            }

            _mapper.Map(enderecoNovoDto, endereco);

            _context.SaveChanges();
        }

        public void DeleteEndereco(int idEndereco)
        {
            Endereco endereco = FetchEnderecosPorId(idEndereco);

            if (endereco == null)
            {
                throw new NotFoundException();
            }

            _context.Remove(endereco);
            _context.SaveChanges();
        }

        private Endereco FetchEnderecosPorId(int idEndereco)
        {
            var enderecoQuery = from e in _context.Enderecos
                                where e.Id == idEndereco
                                select e;

            return enderecoQuery.FirstOrDefault();
        }

    }
}
