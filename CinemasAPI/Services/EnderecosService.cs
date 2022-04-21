using AutoMapper;

using CinemasAPI.Data;
using CinemasAPI.Models;
using CinemasAPI.Exceptions;
using CinemasAPI.Data.Dtos.Endereco;

namespace CinemasAPI.Services
{
    /// <summary>
    ///     Service para as operações e regras de negócio relacionadas aos Enderecos.
    /// </summary>
    internal class EnderecosService
    {
        private AppDbContext _context;
        private IMapper _mapper;

        /// <summary>
        ///     Inicia uma nova instância da classe <see cref="EnderecosService"/>.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="mapper"></param>
        public EnderecosService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        ///     Adiciona um Endereço aos registros.
        /// </summary>
        /// <param name="enderecoDto">DTO para criação de Endereço.</param>
        /// <returns>DTO para leitura de Endereço, com o Endereço criado.</returns>
        public ReadEnderecoDto AddEndereco(CreateEnderecoDto enderecoDto)
        {
            Endereco endereco = _mapper.Map<Endereco>(enderecoDto);

            _context.Enderecos.Add(endereco);
            _context.SaveChanges();

            return _mapper.Map<ReadEnderecoDto>(endereco);
        }

        /// <summary>
        ///     Busca todos os Endereços no banco de dados.
        /// </summary>
        /// <returns>Uma coleçao de todos os endereços, formatado para leitura com DTO.</returns>
        /// <exception cref="NotFoundException">
        ///     Lançada quando não há nenhum registro de Endereço.
        /// </exception>
        public IEnumerable<ReadEnderecoDto> FetchEnderecos()
        {
            var enderecos = _context.Enderecos.Select(e => _mapper.Map<ReadEnderecoDto>(e));

            if (!enderecos.Any())
            {
                throw new NotFoundException();
            }

            return enderecos;
        }

        /// <summary>
        ///     Busca um Endereço no banco de dados com o id passado.
        /// </summary>
        /// <param name="idEndereco">O id respectivo do Endereço no banco de dados.</param>
        /// <returns>O Endereço formatado para leitura com DTO.</returns>
        /// <exception cref="NotFoundException">
        ///     Lançada quando o id passado não corresponde a nenhum endereço.
        /// </exception>
        public ReadEnderecoDto FetchEndereco(int idEndereco)
        {
            var endereco = FetchEnderecosPorId(idEndereco);

            if (endereco == null)
            {
                throw new NotFoundException();
            }

            return _mapper.Map<ReadEnderecoDto>(endereco);
        }

        /// <summary>
        ///     Atualiza um Endereço no banco de dados com o id passado.
        /// </summary>
        /// <param name="idEndereco">O id respectivo do Endereço a ser atualizado.</param>
        /// <param name="enderecoNovoDto">O novo objeto, com os dados a serem alterados.</param>
        /// <exception cref="NotFoundException">
        ///     Lançada quando o id passado não corresponde a nenhum endereço.
        /// </exception>
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

        /// <summary>
        ///     Exclue um registro de Endereço no banco de dados.
        /// </summary>
        /// <param name="idEndereco">O id do Endereço a ser excluído.</param>
        /// <exception cref="NotFoundException">
        ///     Lançada quando o id passado não corresponde a nenhum endereço.
        /// </exception>
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
