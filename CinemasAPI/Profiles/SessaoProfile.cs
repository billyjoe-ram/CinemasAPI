using AutoMapper;
using CinemasAPI.Data.Dtos.Sessao;
using CinemasAPI.Models;

namespace CinemasAPI.Profiles
{
    internal class SessaoProfile : Profile
    {
        public SessaoProfile()
        {
            CreateMap<CreateSessaoDto, Sessao>();
            CreateMap<Sessao, ReadSessaoDto>()
                .ForMember(dto => dto.HorarioFimSessao, opts =>
                    opts.MapFrom(dto =>
                        dto.HorarioInicioSessao.AddMinutes(dto.Filme.DuracaoEmMinutos)
                    )
                );
            CreateMap<UpdateSessaoDto, Sessao>();
        }
    }
}
