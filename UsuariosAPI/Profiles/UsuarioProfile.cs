using AutoMapper;

using UsuariosAPI.Models;
using UsuariosAPI.Data.Dtos;

using Microsoft.AspNetCore.Identity;

namespace UsuariosAPI.Profiles
{
    public class UsuarioProfile : Profile
    {
        public UsuarioProfile()
        {
            CreateMap<CreateUsuarioDto, Usuario>();
            CreateMap<Usuario, IdentityUser<int>>();
        }
    }
}
