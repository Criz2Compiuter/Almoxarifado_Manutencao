﻿using Api_Almoxarifado_Mirvi.Models;

namespace Api_Almoxarifado_Mirvi.DTOs
{
    public class CartDTO
    {
        public CartHeaderDTO CartHeader { get; set; } = new CartHeaderDTO();
        public IEnumerable<CartItemDTO> CartItems { get; set; } = Enumerable.Empty<CartItemDTO>();
    }
}
