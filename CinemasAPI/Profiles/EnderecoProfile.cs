using AutoMapper;

using CinemasAPI.Models;
using CinemasAPI.Data.Dtos.Endereco;

namespace CinemasAPI.Profiles
{
    internal class EnderecoProfile : Profile
    {
        public EnderecoProfile()
        {
            CreateMap<CreateEnderecoDto, Endereco>();
            CreateMap<Endereco, ReadEnderecoDto>();
            CreateMap<UpdateEnderecoDto, Endereco>();
        }
    }
}
