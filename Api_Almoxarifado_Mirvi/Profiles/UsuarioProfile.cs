using Api_Almoxarifado_Mirvi.Data.Dtos;
using Api_Almoxarifado_Mirvi.Models;
using AutoMapper;

namespace Api_Almoxarifado_Mirvi.Profiles
{
    public class UsuarioProfile : Profile
    {
        public UsuarioProfile()
        {
            CreateMap<CreateUsuarioDto, Usuario>();
        }
    }
}
