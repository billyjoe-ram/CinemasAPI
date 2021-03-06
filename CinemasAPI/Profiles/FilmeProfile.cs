using AutoMapper;

using CinemasAPI.Models;
using CinemasAPI.Data.Dtos;
using CinemasAPI.Data.Dtos.Filme;

namespace CinemasAPI.Profiles
{
    public class FilmeProfile : Profile
    {
        public FilmeProfile()
        {
            CreateMap<CreateFilmeDto, Filme>();
            CreateMap<Filme, ReadFilmeDto>();
            CreateMap<UpdateFilmeDto, Filme>();
        }
    }
}
