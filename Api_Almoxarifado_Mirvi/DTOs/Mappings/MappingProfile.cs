using Api_Almoxarifado_Mirvi.Models;
using AutoMapper;

namespace Api_Almoxarifado_Mirvi.DTOs.Mappings {
    public class MappingProfile : Profile {
        public MappingProfile() {
            CreateMap<CartDTO, Cart>().ReverseMap();
            CreateMap<CartHeaderDTO, CartHeader>().ReverseMap();
            CreateMap<CartItemDTO, CartItem>().ReverseMap();
            CreateMap<ProdutoDTO, Produto>().ReverseMap();
        }
    }
}
