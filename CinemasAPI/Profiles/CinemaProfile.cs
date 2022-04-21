using AutoMapper;

using CinemasAPI.Models;
using CinemasAPI.Data.Dtos.Cinema;

namespace CinemasAPI.Profiles
{
    internal class CinemaProfile : Profile
    {
        public CinemaProfile()
        {
            CreateMap<CreateCinemaDto, Cinema>();
            CreateMap<Cinema, ReadCinemaDto>();
            CreateMap<UpdateCinemaDto, Cinema>();
        }
    }
}
